using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TimerFramework;

namespace JoberMQ.Server.Implementation.Schedule.Default
{
    internal class DfSchedule : ScheduleBase
    {
        public DfSchedule(IDbOprService dbOprService, IDboCreator dboCreator) : base(dbOprService, dboCreator)
        {
        }

        public override bool Start()
        {
            try
            {
                jobDataTimer = new TimerFactory().CreateTimer();
                jobDataTimer.Receive += Action;

                var jobSchedules = dbOprService.JobData.GetAll(x => x.IsActive == true && x.IsDelete == false && x.CronTime != null && x.IsCompleted == false
                && (x.ExecuteCountMax == 0 && x.CreatedCount == 0 || x.ExecuteCountMax != x.CreatedCount));
                var timerData = new List<JobDataDbo>();
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
                    timer.TimerGroup = "jobDataSchedule";
                    timer.Data = JsonConvert.SerializeObject(item);

                    timers.Add(timer);
                }

                foreach (var item in timers)
                {
                    var result = jobDataTimer.Add(item);
                    if (result == false)
                    {
                        jobDataTimer.Update(item);
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
            var jobDataDbo = dbOprService.JobData.Get(timer.Id);

            #region SCHEDULE JOB TIMER COMPLETED CHECK
            jobDataDbo.CreatedCount = jobDataDbo.CreatedCount + 1;
            if (jobDataDbo.ExecuteCountMax != null && jobDataDbo.ExecuteCountMax != 0 && jobDataDbo.ExecuteCountMax == jobDataDbo.CreatedCount)
            {
                jobDataDbo.IsCountMax = true;
                jobDataTimer.Remove(jobDataDbo.Id);
            }
            #endregion

            var clones = dboCreator.CloneJobDataToJobs(jobDataDbo);

            foreach (var clone in clones)
            {
                dbOprService.Job.Add(clone);

                foreach (var item in clone.Details)
                {
                    var creatorJobDataDetail = jobDataDbo.Details.FirstOrDefault(x => x.Id == item.CreatedJobDataDetailId);
                    var eventGroupId = Guid.NewGuid();

                    //todo düzelt
                    //if (creatorJobDataDetail.TransportType == TransportTypeEnum.Route)
                    //{
                    //    var createJobMessageDbo = dboCreatorService.JobMessageDboCreate(jobDataDbo, creatorJobDataDetail, clone, item, null);

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
                    //else if (creatorJobDataDetail.TransportType == TransportTypeEnum.Event)
                    //{
                    //    var createJobMessageDbo = dboCreatorService.JobMessageDboCreate(jobDataDbo, creatorJobDataDetail, clone, item, eventGroupId);
                    //    var eventSubscribers = dbOprService.EventSubscriber.GetAll(x => x.EventName == creatorJobDataDetail.EventName);

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
            dbOprService.JobData.Update(jobDataDbo);
            #endregion
        }
    }
}
