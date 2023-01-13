using JoberMQ.Common.Dbos;
using JoberMQ.Database.Abstraction.DbService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TimerFramework;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfSchedule : ScheduleBase
    {
        public DfSchedule(IDatabase database) : base(database)
        {
        }

        public override bool Start()
        {
            try
            {
                jobTimer = new TimerFactory().CreateTimer();
                jobTimer.Receive += Action;

                var jobSchedules = database.Job.GetAll(x => x.IsActive == true && x.IsDelete == false && x.CronTime != null && x.IsCompleted == false
                && (x.ExecuteCountMax == 0 && x.CreatedCount == 0 || x.ExecuteCountMax != x.CreatedCount));
                var timerData = new List<JobDbo>();
                foreach (var item in jobSchedules)
                {
                    timerData.Add(item);
                }

                var timers = new List<TimerModel>();
                foreach (var item in timerData)
                {
                    var timer = new TimerModel();
                    timer.Id = item.Id;
                    timer.CronTime = item.CronTime;
                    timer.TimerGroup = "jobSchedule";
                    timer.Data = JsonConvert.SerializeObject(item);

                    timers.Add(timer);
                }

                foreach (var item in timers)
                {
                    var result = jobTimer.Add(item);
                    if (result == false)
                    {
                        jobTimer.Update(item);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override void Action(TimerModel timer)
        {
            var jobDbo = database.Job.Get(timer.Id);

            #region SCHEDULE JOB TIMER COMPLETED CHECK
            jobDbo.CreatedCount = jobDbo.CreatedCount + 1;
            if (jobDbo.ExecuteCountMax != null && jobDbo.ExecuteCountMax != 0 && jobDbo.ExecuteCountMax == jobDbo.CreatedCount)
            {
                jobDbo.IsCountMax = true;
                jobTimer.Remove(jobDbo.Id);
            }
            #endregion

            var clones = database.DboCreator.CloneJobToJobTransactions(jobDbo);

            foreach (var clone in clones)
            {
                database.JobTransaction.Add(clone.Id, clone);

                foreach (var item in clone.Details)
                {
                    var creatorJobDetail = jobDbo.Details.FirstOrDefault(x => x.Id == item.CreatedJobDetailId);
                    var eventGroupId = Guid.NewGuid();

                    //todo düzelt
                    //if (creatorJobDetail.TransportType == TransportTypeEnum.Route)
                    //{
                    //    var createJobMessageDbo = dboCreatorService.JobMessageDboCreate(jobDbo, creatorJobDetail, clone, item, null);

                    //    if (clone.IsTriggerMain)
                    //    {
                    //        createJobMessageDbo.IsActive = true;
                    //        dbOprService.Message.Add(createJobMessageDbo);
                    //    }
                    //    else
                    //    {
                    //        createJobMessageDbo.IsActive = false;
                    //        dbOprService.Message.Add(createJobMessageDbo);
                    //    }
                    //}
                    //else if (creatorJobDetail.TransportType == TransportTypeEnum.Event)
                    //{
                    //    var createJobMessageDbo = dboCreatorService.JobMessageDboCreate(jobDbo, creatorJobDetail, clone, item, eventGroupId);
                    //    var eventSubscribers = dbOprService.EventSubscriber.GetAll(x => x.EventName == creatorJobDetail.EventName);

                    //    foreach (var itemevent in eventSubscribers)
                    //    {
                    //        if (itemevent.EventSubscriberType == RoutingTypeEnum.Special)
                    //        {
                    //            createJobMessageDbo.ConsumerClientKey = itemevent.ClientKey;
                    //            if (clone.IsTriggerMain)
                    //            {
                    //                createJobMessageDbo.IsActive = true;
                    //                dbOprService.Message.Add(createJobMessageDbo);
                    //            }
                    //            else
                    //            {
                    //                createJobMessageDbo.IsActive = false;
                    //                dbOprService.Message.Add(createJobMessageDbo);
                    //            }
                    //        }
                    //        else if (itemevent.EventSubscriberType == RoutingTypeEnum.Group)
                    //        {
                    //            createJobMessageDbo.ConsumerClientGroupKey = itemevent.ClientGroupKey;
                    //            if (clone.IsTriggerMain)
                    //            {
                    //                createJobMessageDbo.IsActive = true;
                    //                dbOprService.Message.Add(createJobMessageDbo);
                    //            }
                    //            else
                    //            {
                    //                createJobMessageDbo.IsActive = false;
                    //                dbOprService.Message.Add(createJobMessageDbo);
                    //            }
                    //        }
                    //    }
                    //}
                }
            }

            #region UPDATE JobSchedule
            database.Job.Update(jobDbo.Id, jobDbo);
            #endregion
        }
    }
}
