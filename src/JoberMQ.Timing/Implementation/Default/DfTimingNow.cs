using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Database.Abstraction.DbService;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTimingNow : TimingBase
    {
        public DfTimingNow(IMessageBroker messageBroker, IDatabase database) : base(messageBroker, database)
        {
        }

        public override JobAddResponseModel Timing(JobDbo job)
        {
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

            var addJobResult = database.Job.Add(job.Id, job);
            if (!addJobResult)
            {
                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdJobDbo = database.DboCreator.JobTransactionDboCreate(job);
            var addJobTransactionResult = database.JobTransaction.Add(createdJobDbo.Id, createdJobDbo);
            if (!addJobTransactionResult)
            {
                database.Job.Rollback(job.Id, job);

                response.IsSuccess = false;
                response.Message = "JobTransaction eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdMessageDbos = database.DboCreator.MessageDboCreates(createdJobDbo);
            var queueAddResult = messageBroker.QueueAdd(createdMessageDbos);
            if (!queueAddResult)
            {
                database.Job.Rollback(job.Id, job);
                database.JobTransaction.Rollback(createdJobDbo.Id, createdJobDbo);

                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}
