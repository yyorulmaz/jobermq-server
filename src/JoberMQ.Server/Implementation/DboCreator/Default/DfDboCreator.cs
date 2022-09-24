using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Status;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JoberMQ.Server.Implementation.DboCreator.Default
{
    internal class DfDboCreator : IDboCreator
    {
        IDbOprService dbOprService;
        public DfDboCreator(IDbOprService dbOprService)
        {
            this.dbOprService = dbOprService;
        }
        public JobDbo JobDboCreate(JobDataDbo jobDataDbo)
        {
            var jobDbo = new JobDbo();
            jobDbo.Details = new List<JobDetailDbo>();

            #region 0 - BASE
            jobDbo.Id = Guid.NewGuid();
            #endregion

            #region 5 - TIMING
            jobDbo.IsTrigger = jobDataDbo.IsTrigger;
            jobDbo.ErrorWorkflowStop = jobDataDbo.ErrorWorkflowStop;
            jobDbo.TriggerJobId = jobDataDbo.TriggerJobId;
            jobDbo.IsTriggerMain = jobDataDbo.IsTriggerMain;
            jobDbo.TriggerGroupsId = jobDataDbo.TriggerGroupsId;
            #endregion

            #region 6 - RESULT
            jobDbo.IsResultMessageClientSend = false;
            #endregion

            #region 7 - STATUS
            jobDbo.IsCompleted = false;
            jobDbo.IsError = false;
            #endregion

            #region 8 - CLONE CREATED
            jobDbo.Version = jobDataDbo.Version;
            jobDbo.CreatedJobDataId = jobDataDbo.Id;
            #endregion

            #region 99 - CHILD PARENT
            foreach (var item in jobDataDbo.Details)
            {
                var jobDetailDbo = new JobDetailDbo();

                #region 0 - BASE
                jobDetailDbo.Id = Guid.NewGuid();
                #endregion

                #region 3 - MESSAGE

                #region 3.4 - RESULT ROUTING 
                jobDetailDbo.IsResultMessageClientSend = false;
                #endregion

                #endregion

                #region 8 - CLONE CREATED
                jobDetailDbo.CreatedJobDataDetailId = item.Id;
                #endregion

                #region 99 - CHILD PARENT
                jobDetailDbo.JobId = jobDbo.Id;
                #endregion

                jobDbo.Details.Add(jobDetailDbo);
            }
            #endregion

            return jobDbo;
        }
        public MessageDbo MessageDboCreate(JobDataDbo jobDataDbo, JobDataDetailDbo jobDataDetailDbo, JobDbo jobDbo, JobDetailDbo jobDetailDbo, Guid? eventGroupId)
        {
            var messageDbo = new MessageDbo();

            #region 0 - BASE
            messageDbo.Id = Guid.NewGuid();
            #endregion

            #region 1 - PRODUCER
            messageDbo.ProducerClientKey = jobDataDbo.ProducerClientKey;
            messageDbo.ProducerClientGroupKey = jobDataDbo.ProducerClientGroupKey;
            #endregion

            #region 3 - MESSAGE

            #region 3.1 - MESSAGE TYPE
            messageDbo.MessageType = jobDataDetailDbo.MessageType;
            #endregion

            #region 3.2 - MESSAGE 
            messageDbo.Message = jobDataDetailDbo.Message;
            #endregion

            #region 3.3 - ROUTING 
            messageDbo.DistributorKey = jobDataDetailDbo.DistributorKey;
            messageDbo.RoutingKey = jobDataDetailDbo.RoutingKey;
            messageDbo.ConsumerKey = jobDataDetailDbo.ConsumerKey;
            messageDbo.StartsWith = jobDataDetailDbo.StartsWith;
            messageDbo.Contains = jobDataDetailDbo.Contains;
            messageDbo.EndsWith = jobDataDetailDbo.EndsWith;
            #endregion

            #region 3.4 - RESULT ROUTING 
            messageDbo.IsResult = jobDataDetailDbo.IsResult;
            messageDbo.ResultDistributorKey = jobDataDetailDbo.ResultDistributorKey;
            messageDbo.ResultRoutingKey = jobDataDetailDbo.ResultRoutingKey;
            messageDbo.ResultConsumerKey = jobDataDetailDbo.ResultConsumerKey;
            messageDbo.ResultStartsWith = jobDataDetailDbo.ResultStartsWith;
            messageDbo.ResultContains = jobDataDetailDbo.ResultContains;
            messageDbo.ResultEndsWith = jobDataDetailDbo.ResultEndsWith;
            #endregion

            #region 3.5 - OPTION
            messageDbo.Name = jobDataDetailDbo.Name;
            messageDbo.Description = jobDataDetailDbo.Description;
            messageDbo.GeneralData = jobDataDetailDbo.GeneralData;
            messageDbo.PriorityType = jobDataDetailDbo.PriorityType;
            #endregion

            #region 3.6 - CONSUMING
            messageDbo.IsConsumingRetryPause = jobDataDetailDbo.IsConsumingRetryPause;
            messageDbo.ConsumingRetryMaxCount = jobDataDetailDbo.ConsumingRetryMaxCount;
            messageDbo.ConsumingRetryCounter = jobDataDetailDbo.ConsumingRetryCounter;
            messageDbo.ConsumingClientId = null;
            messageDbo.ConsumingClientGroupKey = null;
            #endregion

            #endregion

            #region 5 - TIMING
            messageDbo.TriggerGroupsId = jobDbo.TriggerGroupsId;
            #endregion

            #region 7 - STATUS
            messageDbo.IsError = false;
            messageDbo.StatusTypeMessage = StatusTypeMessageEnum.None;
            messageDbo.TempAgainDate = null;
            #endregion

            #region 8 - CLONE CREATED
            messageDbo.CreatedJobDataId = jobDataDbo.Id;
            messageDbo.CreatedJobDataDetailId = jobDataDetailDbo.Id;
            messageDbo.CreatedJobId = jobDbo.Id;
            messageDbo.CreatedJobDetailId = jobDetailDbo.Id;
            #endregion

            #region 9 - GROUP
            messageDbo.EventGroupsId = eventGroupId;
            #endregion

            return messageDbo;
        }
        public List<JobDbo> CloneJobDataToJobs(JobDataDbo jobData)
        {
            var baseJobs = new List<JobDataDbo>();
            var cloneJobs = new List<JobDbo>();

            if (jobData.IsTrigger)
                baseJobs = dbOprService.JobData.GetAll(x => x.TriggerGroupsId == jobData.TriggerGroupsId);
            else
                baseJobs = dbOprService.JobData.GetAll(x => x.Id == jobData.Id);


            foreach (var item in baseJobs)
            {
                var job = JobDboCreate(item);
                cloneJobs.Add(job);
            }

            if (jobData.IsTrigger)
            {
                var mainJob = cloneJobs.FirstOrDefault(x => x.IsTrigger == true && x.TriggerJobId == null);
                foreach (var item in cloneJobs.OrderBy(x => x.CreateDate))
                {
                    if (item.TriggerJobId != null)
                        item.TriggerJobId = cloneJobs.FirstOrDefault(x => x.CreatedJobDataId == item.TriggerJobId).Id;

                    item.TriggerGroupsId = mainJob.Id;
                }
            }

            return cloneJobs;
        }
    }
}
