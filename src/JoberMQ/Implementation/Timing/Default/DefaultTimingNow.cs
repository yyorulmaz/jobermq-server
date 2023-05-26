using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Implementation.Timing.Default
{
    internal class DefaultTimingNow : TimingBase
    {
        public override async Task<ResponseModel> Timing(JobDbo job)
        {
            var response = new ResponseModel();
            response.IsOnline = true;
            response.Id = job.Id;

            // Job
            var addJobResult = JoberHost.JoberMQ.Database.Job.Add(job.Id, job);
            if (!addJobResult)
            {
                response.IsSucces = false;
                response.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.8.51");
                return response;
            }


            // JobTransaction
            var createdJobTransactionDbo = JoberHost.JoberMQ.Database.DboCreator.JobTransactionDboCreate(job);
            var addJobTransactionResult = JoberHost.JoberMQ.Database.JobTransaction.Add(createdJobTransactionDbo.Id, createdJobTransactionDbo);
            if (!addJobTransactionResult)
            {
                JoberHost.JoberMQ.Database.Job.Rollback(job.Id, job);

                response.IsSucces = false;
                response.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.8.52");
                return response;
            }

            // Messages
            var createdMessageDbos = JoberHost.JoberMQ.Database.DboCreator.MessageDboCreates(createdJobTransactionDbo);
            foreach (var msg in createdMessageDbos)
                await JoberHost.JoberMQ.Distributors.Get(msg.Message.Routing.DistributorKey).Distributoring(msg);

            return response;
        }
    }
}
