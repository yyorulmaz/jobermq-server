using JoberMQ.Common.Dbos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TimerFramework;

namespace JoberMQ.Implementation.Timing.Default
{
    internal class DefaultSchedule : ScheduleBase
    {
        public override bool Start()
        {
            try
            {
                jobTimer = new TimerFactory().CreateTimer();
                jobTimer.Receive += Action;

                var jobSchedules = JoberHost.JoberMQ.Database.Job.GetAll(x => x.IsActive == true && x.IsDelete == false && x.Timing.CronTime != null && x.Status.IsCompleted == false
                && (x.Timing.ExecuteCountMax == 0 && x.Timing.CreatedCount == 0 || x.Timing.ExecuteCountMax != x.Timing.CreatedCount));
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
                    timer.CronTime = item.Timing.CronTime;
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
            #region JOB
            var jobDbo = JoberHost.JoberMQ.Database.Job.Get(timer.Id);
            #endregion

            #region SCHEDULE JOB TIMER COMPLETED CHECK
            jobDbo.Timing.CreatedCount = jobDbo.Timing.CreatedCount + 1;
            if (jobDbo.Timing.ExecuteCountMax != null && jobDbo.Timing.ExecuteCountMax != 0 && jobDbo.Timing.ExecuteCountMax == jobDbo.Timing.CreatedCount)
            {
                jobDbo.Timing.IsCountMax = true;
                jobTimer.Remove(jobDbo.Id);
            }
            #endregion

            var clones = JoberHost.JoberMQ.Database.DboCreator.CloneJobToJobTransactions(jobDbo);

            //foreach (var clone in clones)
            //{
            //    JoberHost.JoberMQ.Database.JobTransaction.Add(clone.Id, clone);

            //    foreach (var item in clone.JobTransactioDetails)
            //    {
            //        var creatorJobDetail = jobDbo.JobDetails.FirstOrDefault(x => x.Id == item.CreatedJobDetailId);
            //        var eventGroupId = Guid.NewGuid();
            //    }
            //}

            foreach (var clone in clones)
            {
                JoberHost.JoberMQ.Database.JobTransaction.Add(clone.Id, clone);

                foreach (var item in clone.JobTransactionDetails)
                {
                    var creatorJobDetail = jobDbo.JobDetails.FirstOrDefault(x => x.Id == item.CreatedJobDetailId);
                    var eventGroupId = Guid.NewGuid();


                    var createdMessageDbo = JoberHost.JoberMQ.Database.DboCreator.MessageDboCreate(jobDbo, creatorJobDetail, clone, item, eventGroupId);

                    if (clone.Timing.IsTriggerMain)
                    {
                        createdMessageDbo.IsActive = true;
                        var result = JoberHost.JoberMQ.Distributors.Get(createdMessageDbo.Message.Routing.DistributorKey).Distributoring(createdMessageDbo).Result;
                    }
                    else
                    {
                        createdMessageDbo.IsActive = false;
                        var addMessage = JoberHost.JoberMQ.Database.Message.Add(createdMessageDbo.Id, createdMessageDbo);
                    }
                }
            }




            #region UPDATE JobSchedule
            JoberHost.JoberMQ.Database.Job.Update(jobDbo.Id, jobDbo);
            #endregion
        }
    }
}
