using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Producer;
using JoberMQ.Common.Models.Status;
using JoberMQ.Abstraction.Database;

namespace JoberMQ.Implementation.Database.Default
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
            //var jobTransactionDbo = new JobTransactionDbo();
            //jobTransactionDbo.JobTransactionDetails = new List<JobTransactionDetailDbo>();

            //jobTransactionDbo.Id = Guid.NewGuid();
            //jobTransactionDbo.Timing = jobDbo.Timing;
            //jobTransactionDbo.Status = jobDbo.Status;
            //jobTransactionDbo.IsResultMessageClientSend = false;
            //jobTransactionDbo.CreatedJobId = jobDbo.Id;

            //foreach (var item in jobDbo.JobDetails)
            //{
            //    var jobTransactionDetailDbo = new JobTransactionDetailDbo();

            //    jobTransactionDetailDbo.Id = Guid.NewGuid();
            //    jobTransactionDetailDbo.IsResultMessageClientSend = false;
            //    jobTransactionDetailDbo.CreatedJobId = jobDbo.Id;
            //    jobTransactionDetailDbo.CreatedJobDetailId = item.Id;

            //    jobTransactionDbo.JobTransactionDetails.Add(jobTransactionDetailDbo);
            //}

            //jobTransactionDbo.IsDbTextSave = jobDbo.IsDbTextSave;

            //return jobTransactionDbo;



            var jobTransactionDbo = new JobTransactionDbo
            {
                Id = Guid.NewGuid(),
                Timing = jobDbo.Timing,
                Status = jobDbo.Status,
                IsResultMessageClientSend = false,
                CreatedJobId = jobDbo.Id,
                IsDbTextSave = jobDbo.IsDbTextSave,
                JobTransactionDetails = jobDbo.JobDetails.Select(item => new JobTransactionDetailDbo
                {
                    Id = Guid.NewGuid(),
                    IsResultMessageClientSend = false,
                    CreatedJobId = jobDbo.Id,
                    CreatedJobDetailId = item.Id
                }).ToList()
            };

            return jobTransactionDbo;
        }
        public MessageDbo MessageDboCreate(JobDbo jobDbo, JobDetailDbo jobDetailDbo, JobTransactionDbo jobTransactionDbo, JobTransactionDetailDbo jobTransactionDetailDbo, Guid? eventGroupId)
        {
            var messageDbo = new MessageDbo();

            messageDbo.Id = Guid.NewGuid();
            messageDbo.Operation = jobDbo.Operation;
            messageDbo.Producer = jobDbo.Producer;
            messageDbo.Message = jobDetailDbo.Message;
            messageDbo.IsResult = jobDetailDbo.IsResultMessage;
            messageDbo.ResultMessage = jobDetailDbo.ResultMessage;
            messageDbo.TriggerGroupsId = jobTransactionDbo.Timing.TriggerGroupsId;
            messageDbo.CreatedJobId = jobDbo.Id;
            messageDbo.CreatedJobDetailId = jobDetailDbo.Id;
            messageDbo.CreatedJobTransactionId = jobTransactionDbo.Id;
            messageDbo.CreatedJobTransactionDetailId = jobTransactionDetailDbo.Id;
            messageDbo.EventGroupsId = eventGroupId;

            messageDbo.Status = new StatusModel();
            messageDbo.Status.IsError = false;
            messageDbo.Status.StatusTypeMessage = StatusTypeMessageEnum.None;
            messageDbo.Status.TempAgainDate = null;
            messageDbo.IsDbTextSave = jobDbo.IsDbTextSave;
            return messageDbo;
        }
        public List<MessageDbo> MessageDboCreates(JobTransactionDbo jobTransactionDbo)
        {
            var messageDbos = new List<MessageDbo>();
            var creatorJob = jobDbOpr.Get(jobTransactionDbo.CreatedJobId);

            foreach (var item in jobTransactionDbo.JobTransactionDetails)
            {
                var messageDbo = new MessageDbo();
                var jobDetailDbo = creatorJob.JobDetails.Where(x => x.Id == item.CreatedJobDetailId).FirstOrDefault();


                messageDbo.Id = Guid.NewGuid();

                messageDbo.Producer = new ProducerModel();
                messageDbo.Producer = creatorJob.Producer;


                messageDbo.Message = jobDetailDbo.Message;
                messageDbo.IsResult = jobDetailDbo.IsResultMessage;
                messageDbo.ResultMessage = jobDetailDbo.ResultMessage;

                //messageDbo.TriggerGroupsId = jobTransactionDbo.TriggerGroupsId;

                messageDbo.CreatedJobId = jobTransactionDbo.CreatedJobId;
                messageDbo.CreatedJobDetailId = item.CreatedJobDetailId;
                messageDbo.CreatedJobTransactionId = jobTransactionDbo.Id;
                messageDbo.CreatedJobTransactionDetailId = item.Id;
                //messageDbo.EventGroupsId = eventGroupId;

                messageDbo.Status = new StatusModel();
                messageDbo.Status.IsError = false;
                messageDbo.Status.StatusTypeMessage = StatusTypeMessageEnum.None;
                messageDbo.Status.TempAgainDate = null;

                messageDbo.IsDbTextSave = jobTransactionDbo.IsDbTextSave;

                messageDbos.Add(messageDbo);
            }

            return messageDbos;


        }
        public List<JobTransactionDbo> CloneJobToJobTransactions(JobDbo job)
        {
            #region eski
            //var baseJobs = new List<JobDbo>();
            //var cloneJobs = new List<JobTransactionDbo>();

            //if (job.Timing.IsTrigger)
            //    baseJobs = jobDbOpr.GetAll(x => x.Timing.TriggerGroupsId == job.Timing.TriggerGroupsId);
            //else
            //    baseJobs = jobDbOpr.GetAll(x => x.Id == job.Id);


            //foreach (var item in baseJobs)
            //{
            //    var jobTransaction = JobTransactionDboCreate(item);
            //    cloneJobs.Add(jobTransaction);
            //}

            //if (job.Timing.IsTrigger)
            //{
            //    var mainJob = cloneJobs.FirstOrDefault(x => x.Timing.IsTrigger == true && x.Timing.TriggerJobId == null);
            //    foreach (var item in cloneJobs.OrderBy(x => x.CreateDate))
            //    {
            //        if (item.Timing.TriggerJobId != null)
            //            item.Timing.TriggerJobId = cloneJobs.FirstOrDefault(x => x.CreatedJobId == item.Timing.TriggerJobId).Id;

            //        item.Timing.TriggerGroupsId = mainJob.Id;
            //    }
            //}

            //return cloneJobs; 
            #endregion

            var baseJobs = new List<JobDbo>();
            var cloneJobs = new List<JobTransactionDbo>();

            if (job.Timing.IsTrigger)
            {
                baseJobs = jobDbOpr.GetAll(x => x.Timing.TriggerGroupsId == job.Timing.TriggerGroupsId);
            }
            else
            {
                baseJobs.Add(job);
            }

            foreach (var baseJob in baseJobs)
            {
                var jobTransaction = JobTransactionDboCreate(baseJob);
                cloneJobs.Add(jobTransaction);
            }

            if (job.Timing.IsTrigger)
            {
                var mainJob = cloneJobs.FirstOrDefault(x => x.Timing.IsTrigger && x.Timing.TriggerJobId == null);
                foreach (var item in cloneJobs.OrderBy(x => x.CreateDate))
                {
                    if (item.Timing.TriggerJobId != null)
                    {
                        var matchingJob = cloneJobs.FirstOrDefault(x => x.CreatedJobId == item.Timing.TriggerJobId);
                        if (matchingJob != null)
                        {
                            item.Timing.TriggerJobId = matchingJob.Id;
                        }
                    }
                    item.Timing.TriggerGroupsId = mainJob?.Id;
                }
            }

            return cloneJobs;
        }



        public JobTransactionDbo Create(JobDbo jobDbo)
        {
            var result = new JobTransactionDbo();
            result.JobTransactionDetails = new List<JobTransactionDetailDbo>();

            result.Id = Guid.NewGuid();
            result.Timing = jobDbo.Timing;
            result.Status = jobDbo.Status;
            result.IsResultMessageClientSend = false;
            result.CreatedJobId = jobDbo.Id;
            result.IsDbTextSave = jobDbo.IsDbTextSave;


            //foreach (var item in jobTransactionDbo.JobTransactioDetails)
            foreach (var item in jobDbo.JobDetails)
            {
                var detail = new JobTransactionDetailDbo();

                detail.Id = Guid.NewGuid();
                detail.IsResultMessageClientSend = false;
                detail.CreatedJobId = jobDbo.Id;
                detail.CreatedJobDetailId = item.Id;
                detail.IsDbTextSave = item.IsDbTextSave;

                result.JobTransactionDetails.Add(detail);
            }

            return result;
        }
    }
}
