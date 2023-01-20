using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
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
            jobTransactionDbo.JobTransactioDetails = new List<JobTransactionDetailDbo>();

            jobTransactionDbo.Id = Guid.NewGuid();

            jobTransactionDbo.Timing = jobDbo.Timing;
            jobTransactionDbo.Status = jobDbo.Status;
            jobTransactionDbo.Version = jobDbo.Version;
            jobTransactionDbo.CreatedJobId = jobDbo.Id;

            jobTransactionDbo.IsResultMessageClientSend = false;

            foreach (var item in jobTransactionDbo.JobTransactioDetails)
            {
                var jobTransactionDetailDbo = new JobTransactionDetailDbo();
                
                jobTransactionDetailDbo.Id = Guid.NewGuid();
                jobTransactionDetailDbo.IsResultMessageClientSend = false;
                jobTransactionDetailDbo.CreatedJobId = jobDbo.Id;
                jobTransactionDetailDbo.CreatedJobDetailId = item.Id;

                jobTransactionDbo.JobTransactioDetails.Add(jobTransactionDetailDbo);
            }

            return jobTransactionDbo;
        }
        public MessageDbo MessageDboCreate(JobDbo jobDbo, JobDetailDbo jobDetailDbo, JobTransactionDbo jobTransactionDbo, JobTransactionDetailDbo jobTransactionDetailDbo, Guid? eventGroupId)
        {
            var messageDbo = new MessageDbo();

            messageDbo.Id = Guid.NewGuid();
            messageDbo.Operation = jobDbo.Operation;
            messageDbo.Producer = jobDbo.Producer;
            messageDbo.Message = jobDetailDbo.Message;
            messageDbo.IsResult = jobDetailDbo.IsResult;
            messageDbo.ResultMessage = jobDetailDbo.ResultMessage;
            messageDbo.Consuming = jobDetailDbo.Consuming;
            messageDbo.TriggerGroupsId = jobTransactionDbo.Timing.TriggerGroupsId;
            messageDbo.CreatedJobId = jobDbo.Id;
            messageDbo.CreatedJobDetailId = jobDetailDbo.Id;
            messageDbo.CreatedJobTransactionId = jobTransactionDbo.Id;
            messageDbo.CreatedJobTransactionDetailId = jobTransactionDetailDbo.Id;
            messageDbo.EventGroupsId = eventGroupId;
            messageDbo.Status.IsError = false;
            messageDbo.Status.StatusTypeMessage = StatusTypeMessageEnum.None;
            messageDbo.Status.TempAgainDate = null;

            //toto kontrol
            //messageDbo.Consuming = ;


            return messageDbo;
        }
        public List<MessageDbo> MessageDboCreates(JobTransactionDbo jobTransactionDbo)
        {
            //var messageDbos = new List<MessageDbo>();
            //var creatorJob = jobDbOpr.Get(jobTransactionDbo.CreatedJobId);

            //foreach (var item in jobTransactionDbo.JobTransactioDetails)
            //{
            //    var messageDbo = new MessageDbo();

            //    #region 0 - BASE
            //    messageDbo.Id = Guid.NewGuid();
            //    #endregion

            //    #region 1 - PRODUCER
            //    messageDbo.Producer.ClientKey = creatorJob.Producer.ClientKey;
            //    messageDbo.Producer.ClientGroupKey = creatorJob.Producer.ClientGroupKey;
            //    #endregion

            //    #region 3 - MESSAGE

            //    var jobDetailDbo = creatorJob.JobDetails.Where(x => x.Id == item.CreatedJobDetailId).FirstOrDefault();

            //    #region 3.1 - MESSAGE TYPE
            //    messageDbo.MessageType = jobDetailDbo.MessageType;
            //    #endregion

            //    #region 3.2 - MESSAGE 
            //    messageDbo.Message = jobDetailDbo.Message;
            //    #endregion

            //    #region 3.3 - ROUTING 
            //    messageDbo.Routing.RoutingType = jobDetailDbo.Routing.RoutingType;
            //    messageDbo.Routing.DistributorKey = jobDetailDbo.Routing.DistributorKey;
            //    messageDbo.Routing.RoutingKey = jobDetailDbo.Routing.RoutingKey;
            //    messageDbo.Routing.ConsumerKey = jobDetailDbo.Routing.ConsumerKey;
            //    messageDbo.Routing.StartsWith = jobDetailDbo.Routing.StartsWith;
            //    messageDbo.Routing.Contains = jobDetailDbo.Routing.Contains;
            //    messageDbo.Routing.EndsWith = jobDetailDbo.Routing.EndsWith;
            //    #endregion

            //    #region 3.4 - RESULT ROUTING 
            //    messageDbo.ResultRouting.RoutingType = jobDetailDbo.ResultRouting.RoutingType;
            //    messageDbo.ResultRouting.IsResult = jobDetailDbo.ResultRouting.IsResult;
            //    messageDbo.ResultRouting.DistributorKey = jobDetailDbo.ResultRouting.DistributorKey;
            //    messageDbo.ResultRouting.RoutingKey = jobDetailDbo.ResultRouting.RoutingKey;
            //    messageDbo.ResultRouting.ConsumerKey = jobDetailDbo.ResultRouting.ConsumerKey;
            //    messageDbo.ResultRouting.StartsWith = jobDetailDbo.ResultRouting.StartsWith;
            //    messageDbo.ResultRouting.Contains = jobDetailDbo.ResultRouting.Contains;
            //    messageDbo.ResultRouting.EndsWith = jobDetailDbo.ResultRouting.EndsWith;
            //    #endregion

            //    #region 3.5 - OPTION
            //    messageDbo.Info.Name = jobDetailDbo.Info.Name;
            //    messageDbo.Info.Description = jobDetailDbo.Info.Description;
            //    messageDbo.Info.GeneralData = jobDetailDbo.Info.GeneralData;
            //    messageDbo.PriorityType = jobDetailDbo.PriorityType;
            //    #endregion

            //    #region 3.6 - CONSUMING
            //    messageDbo.Consuming.IsConsumingRetryPause = jobDetailDbo.Consuming.IsConsumingRetryPause;
            //    messageDbo.Consuming.ConsumingRetryMaxCount = jobDetailDbo.Consuming.ConsumingRetryMaxCount;
            //    messageDbo.Consuming.ConsumingRetryCounter = jobDetailDbo.Consuming.ConsumingRetryCounter;
            //    messageDbo.Consuming.ClientKey = null;
            //    messageDbo.Consuming.ClientGroupKey = null;
            //    #endregion

            //    #endregion

            //    #region 5 - TIMING
            //    messageDbo.TriggerGroupsId = jobTransactionDbo.TriggerGroupsId;
            //    #endregion

            //    #region 7 - STATUS
            //    messageDbo.Status.IsError = false;
            //    messageDbo.Status.StatusTypeMessage = StatusTypeMessageEnum.None;
            //    messageDbo.Status.TempAgainDate = null;
            //    #endregion

            //    #region 8 - CLONE CREATED
            //    messageDbo.CreatedJobId = jobTransactionDbo.CreatedJobId;
            //    messageDbo.CreatedJobDetailId = item.CreatedJobDetailId;
            //    messageDbo.CreatedJobTransactionId = jobTransactionDbo.Id;
            //    messageDbo.CreatedJobTransactionDetailId = item.Id;
            //    #endregion

            //    #region 9 - GROUP
            //    //todo kontrol
            //    //messageDbo.EventGroupsId = eventGroupId;
            //    #endregion

            //    messageDbos.Add(messageDbo);
            //}

            //return messageDbos;


            return null;
        }




        public List<JobTransactionDbo> CloneJobToJobTransactions(JobDbo job)
        {
            var baseJobs = new List<JobDbo>();
            var cloneJobs = new List<JobTransactionDbo>();

            if (job.Timing.IsTrigger)
                baseJobs = jobDbOpr.GetAll(x => x.Timing.TriggerGroupsId == job.Timing.TriggerGroupsId);
            else
                baseJobs = jobDbOpr.GetAll(x => x.Id == job.Id);


            foreach (var item in baseJobs)
            {
                var jobTransaction = JobTransactionDboCreate(item);
                cloneJobs.Add(jobTransaction);
            }

            if (job.Timing.IsTrigger)
            {
                var mainJob = cloneJobs.FirstOrDefault(x => x.Timing.IsTrigger == true && x.Timing.TriggerJobId == null);
                foreach (var item in cloneJobs.OrderBy(x => x.CreateDate))
                {
                    if (item.Timing.TriggerJobId != null)
                        item.Timing.TriggerJobId = cloneJobs.FirstOrDefault(x => x.CreatedJobId == item.Timing.TriggerJobId).Id;

                    item.Timing.TriggerGroupsId = mainJob.Id;
                }
            }

            return cloneJobs;
        }
    }
}
