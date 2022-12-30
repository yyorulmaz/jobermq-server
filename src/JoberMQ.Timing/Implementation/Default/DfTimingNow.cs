using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbService;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTimingNow : TimingBase
    {
        public DfTimingNow(IMessageBroker messageBroker, IDatabaseService databaseService) : base(messageBroker, databaseService)
        {
        }

        public override JobAddResponseModel Timing(JobDbo job)
        {
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

            var addJobResult = databaseService.Job.Add(job);
            if (!addJobResult)
            {
                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdJobDbo = databaseService.DboCreator.JobTransactionDboCreate(job);
            var addJobTransactionResult = databaseService.JobTransaction.Add(createdJobDbo);
            if (!addJobTransactionResult)
            {
                databaseService.Job.Rollback(job);

                response.IsSuccess = false;
                response.Message = "JobTransaction eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdMessageDbos = databaseService.DboCreator.MessageDboCreates(createdJobDbo);
            var queueAddResult = messageBroker.QueueAdd(createdMessageDbos);
            if (!queueAddResult)
            {
                databaseService.Job.Rollback(job);
                databaseService.JobTransaction.Rollback(createdJobDbo);

                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}
