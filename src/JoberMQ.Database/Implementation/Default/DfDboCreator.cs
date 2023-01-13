using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Database.Abstraction.DboCreator;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JoberMQ.Database.Implementation.DboCreator.Default
{
    internal class DfDboCreator : IDboCreator
    {
        IOprRepositoryGuid<JobDbo> jobDbOpr;
        public DfDboCreator(IOprRepositoryGuid<JobDbo> jobDbOpr)
        {
            this.jobDbOpr = jobDbOpr;
        }
        public JobTransactionDbo JobTransactionDboCreate(JobDbo jobDbo)
        {
            var jobTransactionDbo = new JobTransactionDbo();
            jobTransactionDbo.Details = new List<JobTransactionDetailDbo>();

            #region 0 - BASE
            jobTransactionDbo.Id = Guid.NewGuid();
            #endregion

            #region 5 - TIMING
            jobTransactionDbo.IsTrigger = jobTransactionDbo.IsTrigger;
            jobTransactionDbo.ErrorWorkflowStop = jobTransactionDbo.ErrorWorkflowStop;
            jobTransactionDbo.TriggerJobId = jobTransactionDbo.TriggerJobId;
            jobTransactionDbo.IsTriggerMain = jobTransactionDbo.IsTriggerMain;
            jobTransactionDbo.TriggerGroupsId = jobTransactionDbo.TriggerGroupsId;
            #endregion

            #region 6 - RESULT
            jobTransactionDbo.IsResultMessageClientSend = false;
            #endregion

            #region 7 - STATUS
            jobTransactionDbo.IsCompleted = false;
            jobTransactionDbo.IsError = false;
            #endregion

            #region 8 - CLONE CREATED
            jobTransactionDbo.Version = jobTransactionDbo.Version;
            jobTransactionDbo.CreatedJobId = jobTransactionDbo.Id;
            #endregion

            #region 99 - CHILD PARENT
            foreach (var item in jobTransactionDbo.Details)
            {
                var jobTransactionDetailDbo = new JobTransactionDetailDbo();

                #region 0 - BASE
                jobTransactionDetailDbo.Id = Guid.NewGuid();
                #endregion

                #region 3 - MESSAGE

                #region 3.4 - RESULT ROUTING 
                jobTransactionDetailDbo.IsResultMessageClientSend = false;
                #endregion

                #endregion

                #region 8 - CLONE CREATED
                jobTransactionDetailDbo.CreatedJobDetailId = item.Id;
                #endregion

                #region 99 - CHILD PARENT
                jobTransactionDetailDbo.JobId = jobTransactionDbo.Id;
                #endregion

                jobTransactionDbo.Details.Add(jobTransactionDetailDbo);
            }
            #endregion

            return jobTransactionDbo;
        }
        public MessageDbo MessageDboCreate(JobDbo jobDbo, JobDetailDbo jobDetailDbo, JobTransactionDbo jobTransactionDbo, JobTransactionDetailDbo jobTransactionDetailDbo, Guid? eventGroupId)
        {
            var messageDbo = new MessageDbo();

            #region 0 - BASE
            messageDbo.Id = Guid.NewGuid();
            #endregion

            #region 1 - PRODUCER
            messageDbo.ProducerClientKey = jobDbo.ProducerClientKey;
            messageDbo.ProducerClientGroupKey = jobDbo.ProducerClientGroupKey;
            #endregion

            #region 3 - MESSAGE

            #region 3.1 - MESSAGE TYPE
            messageDbo.MessageType = jobDetailDbo.MessageType;
            #endregion

            #region 3.2 - MESSAGE 
            messageDbo.Message = jobDetailDbo.Message;
            #endregion

            #region 3.3 - ROUTING 
            messageDbo.RoutingType = jobDetailDbo.RoutingType;
            messageDbo.DistributorKey = jobDetailDbo.DistributorKey;
            messageDbo.RoutingKey = jobDetailDbo.RoutingKey;
            messageDbo.ConsumerKey = jobDetailDbo.ConsumerKey;
            messageDbo.StartsWith = jobDetailDbo.StartsWith;
            messageDbo.Contains = jobDetailDbo.Contains;
            messageDbo.EndsWith = jobDetailDbo.EndsWith;
            #endregion

            #region 3.4 - RESULT ROUTING 
            messageDbo.ResultRoutingType = jobDetailDbo.ResultRoutingType;
            messageDbo.IsResult = jobDetailDbo.IsResult;
            messageDbo.ResultDistributorKey = jobDetailDbo.ResultDistributorKey;
            messageDbo.ResultRoutingKey = jobDetailDbo.ResultRoutingKey;
            messageDbo.ResultConsumerKey = jobDetailDbo.ResultConsumerKey;
            messageDbo.ResultStartsWith = jobDetailDbo.ResultStartsWith;
            messageDbo.ResultContains = jobDetailDbo.ResultContains;
            messageDbo.ResultEndsWith = jobDetailDbo.ResultEndsWith;
            #endregion

            #region 3.5 - OPTION
            messageDbo.Name = jobDetailDbo.Name;
            messageDbo.Description = jobDetailDbo.Description;
            messageDbo.GeneralData = jobDetailDbo.GeneralData;
            messageDbo.PriorityType = jobDetailDbo.PriorityType;
            #endregion

            #region 3.6 - CONSUMING
            messageDbo.IsConsumingRetryPause = jobDetailDbo.IsConsumingRetryPause;
            messageDbo.ConsumingRetryMaxCount = jobDetailDbo.ConsumingRetryMaxCount;
            messageDbo.ConsumingRetryCounter = jobDetailDbo.ConsumingRetryCounter;
            messageDbo.ConsumingClientId = null;
            messageDbo.ConsumingClientGroupKey = null;
            #endregion

            #endregion

            #region 5 - TIMING
            messageDbo.TriggerGroupsId = jobTransactionDbo.TriggerGroupsId;
            #endregion

            #region 7 - STATUS
            messageDbo.IsError = false;
            messageDbo.StatusTypeMessage = StatusTypeMessageEnum.None;
            messageDbo.TempAgainDate = null;
            #endregion

            #region 8 - CLONE CREATED
            messageDbo.CreatedJobId = jobDbo.Id;
            messageDbo.CreatedJobDetailId = jobDetailDbo.Id;
            messageDbo.CreatedJobTransactionId = jobTransactionDbo.Id;
            messageDbo.CreatedJobTransactionDetailId = jobTransactionDetailDbo.Id;
            #endregion

            #region 9 - GROUP
            messageDbo.EventGroupsId = eventGroupId;
            #endregion

            return messageDbo;
        }
        public List<MessageDbo> MessageDboCreates(JobTransactionDbo jobTransactionDbo)
        {
            var messageDbos = new List<MessageDbo>();
            var creatorJob = jobDbOpr.Get(jobTransactionDbo.CreatedJobId);

            foreach (var item in jobTransactionDbo.Details)
            {
                var messageDbo = new MessageDbo();

                #region 0 - BASE
                messageDbo.Id = Guid.NewGuid();
                #endregion

                #region 1 - PRODUCER
                messageDbo.ProducerClientKey = creatorJob.ProducerClientKey;
                messageDbo.ProducerClientGroupKey = creatorJob.ProducerClientGroupKey;
                #endregion

                #region 3 - MESSAGE

                var jobDetailDbo = creatorJob.Details.Where(x => x.Id == item.CreatedJobDetailId).FirstOrDefault();

                #region 3.1 - MESSAGE TYPE
                messageDbo.MessageType = jobDetailDbo.MessageType;
                #endregion

                #region 3.2 - MESSAGE 
                messageDbo.Message = jobDetailDbo.Message;
                #endregion

                #region 3.3 - ROUTING 
                messageDbo.RoutingType = jobDetailDbo.RoutingType;
                messageDbo.DistributorKey = jobDetailDbo.DistributorKey;
                messageDbo.RoutingKey = jobDetailDbo.RoutingKey;
                messageDbo.ConsumerKey = jobDetailDbo.ConsumerKey;
                messageDbo.StartsWith = jobDetailDbo.StartsWith;
                messageDbo.Contains = jobDetailDbo.Contains;
                messageDbo.EndsWith = jobDetailDbo.EndsWith;
                #endregion

                #region 3.4 - RESULT ROUTING 
                messageDbo.ResultRoutingType = jobDetailDbo.ResultRoutingType;
                messageDbo.IsResult = jobDetailDbo.IsResult;
                messageDbo.ResultDistributorKey = jobDetailDbo.ResultDistributorKey;
                messageDbo.ResultRoutingKey = jobDetailDbo.ResultRoutingKey;
                messageDbo.ResultConsumerKey = jobDetailDbo.ResultConsumerKey;
                messageDbo.ResultStartsWith = jobDetailDbo.ResultStartsWith;
                messageDbo.ResultContains = jobDetailDbo.ResultContains;
                messageDbo.ResultEndsWith = jobDetailDbo.ResultEndsWith;
                #endregion

                #region 3.5 - OPTION
                messageDbo.Name = jobDetailDbo.Name;
                messageDbo.Description = jobDetailDbo.Description;
                messageDbo.GeneralData = jobDetailDbo.GeneralData;
                messageDbo.PriorityType = jobDetailDbo.PriorityType;
                #endregion

                #region 3.6 - CONSUMING
                messageDbo.IsConsumingRetryPause = jobDetailDbo.IsConsumingRetryPause;
                messageDbo.ConsumingRetryMaxCount = jobDetailDbo.ConsumingRetryMaxCount;
                messageDbo.ConsumingRetryCounter = jobDetailDbo.ConsumingRetryCounter;
                messageDbo.ConsumingClientId = null;
                messageDbo.ConsumingClientGroupKey = null;
                #endregion

                #endregion

                #region 5 - TIMING
                messageDbo.TriggerGroupsId = jobTransactionDbo.TriggerGroupsId;
                #endregion

                #region 7 - STATUS
                messageDbo.IsError = false;
                messageDbo.StatusTypeMessage = StatusTypeMessageEnum.None;
                messageDbo.TempAgainDate = null;
                #endregion

                #region 8 - CLONE CREATED
                messageDbo.CreatedJobId = jobTransactionDbo.CreatedJobId;
                messageDbo.CreatedJobDetailId = item.CreatedJobDetailId;
                messageDbo.CreatedJobTransactionId = jobTransactionDbo.Id;
                messageDbo.CreatedJobTransactionDetailId = item.Id;
                #endregion

                #region 9 - GROUP
                //todo kontrol
                //messageDbo.EventGroupsId = eventGroupId;
                #endregion

                messageDbos.Add(messageDbo);
            }

            return messageDbos;
        }




        public List<JobTransactionDbo> CloneJobToJobTransactions(JobDbo job)
        {
            var baseJobs = new List<JobDbo>();
            var cloneJobs = new List<JobTransactionDbo>();

            if (job.IsTrigger)
                baseJobs = jobDbOpr.GetAll(x => x.TriggerGroupsId == job.TriggerGroupsId);
            else
                baseJobs = jobDbOpr.GetAll(x => x.Id == job.Id);


            foreach (var item in baseJobs)
            {
                var jobTransaction = JobTransactionDboCreate(item);
                cloneJobs.Add(jobTransaction);
            }

            if (job.IsTrigger)
            {
                var mainJob = cloneJobs.FirstOrDefault(x => x.IsTrigger == true && x.TriggerJobId == null);
                foreach (var item in cloneJobs.OrderBy(x => x.CreateDate))
                {
                    if (item.TriggerJobId != null)
                        item.TriggerJobId = cloneJobs.FirstOrDefault(x => x.CreatedJobId == item.TriggerJobId).Id;

                    item.TriggerGroupsId = mainJob.Id;
                }
            }

            return cloneJobs;
        }
    }
}
