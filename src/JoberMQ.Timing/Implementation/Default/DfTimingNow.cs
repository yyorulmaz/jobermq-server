using JoberMQ.Broker.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using System.Threading.Tasks;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTimingNow : TimingBase
    {
        public DfTimingNow(IMessageBroker messageBroker, IDatabase database, IStatusCode statusCode) : base(messageBroker, database, statusCode)
        {
        }

        
        public override async Task<ResponseModel> Timing(JobDbo job)
        {
            var response = new ResponseModel();
            response.IsOnline = true;
            response.Id = job.Id;


            // Job
            var addJobResult = database.Job.Add(job.Id, job);
            if (!addJobResult)
            {
                response.IsSucces = false;
                response.Message = statusCode.GetStatusMessage("1.8.51");
                return response;
            }


            // JobTransaction
            var createdJobTransactionDbo = database.DboCreator.JobTransactionDboCreate(job);
            var addJobTransactionResult = database.JobTransaction.Add(createdJobTransactionDbo.Id, createdJobTransactionDbo);
            if (!addJobTransactionResult)
            {
                database.Job.Rollback(job.Id, job);

                response.IsSucces = false;
                response.Message = statusCode.GetStatusMessage("1.8.52");
                return response;
            }


            // Messages
            var createdMessageDbos = database.DboCreator.MessageDboCreates(createdJobTransactionDbo);
            foreach (var msg in createdMessageDbos)
                await messageBroker.Brokering(msg);










            //var createdMessageDbos = database.DboCreator.MessageDboCreates(createdJobTransactionDbo);
            //var queueAddResult = MessageAdd(createdMessageDbos);
            //if (!queueAddResult)
            //{
            //    database.Job.Rollback(job.Id, job);
            //    database.JobTransaction.Rollback(createdJobTransactionDbo.Id, createdJobTransactionDbo);

            //    response.IsSucces = false;
            //    response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
            //    return response;
            //}

            //response.IsSucces = true;
            //return response;

            return response;
        }


    }
}
