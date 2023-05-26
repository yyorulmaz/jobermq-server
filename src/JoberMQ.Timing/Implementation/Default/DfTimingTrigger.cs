using JoberMQ.Database.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.StatusCode.Abstraction;
using System.Threading.Tasks;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTimingTrigger : TimingBase
    {
        public DfTimingTrigger(IDatabase database, IStatusCode statusCode) : base(database, statusCode)
        {
        }

        public override async Task<ResponseModel> Timing(JobDbo job)
        {
            var response = new ResponseModel();
            response.IsOnline = true;
            response.Id = job.Id;

            var triggeredJob = database.Job.Get(job.Timing.TriggerJobId.Value);
            var beforeTriggerGroupsId = triggeredJob.Timing.TriggerGroupsId;
            var beforeIsTriggerMain = triggeredJob.Timing.IsTriggerMain;
            var beforeIsTrigger = triggeredJob.Timing.IsTrigger;

            if (triggeredJob.Timing.TriggerJobId == null)
            {
                triggeredJob.Timing.TriggerGroupsId = triggeredJob.Id;
                triggeredJob.Timing.IsTriggerMain = true;
            }
            triggeredJob.Timing.IsTrigger = true;

            var triggeredJobResult = database.Job.Update(triggeredJob.Id, triggeredJob);
            if (!triggeredJobResult)
            {
                response.IsSucces = false;
                response.Message = "Triggerred Job güncellenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            job.Timing.TriggerGroupsId = triggeredJob.Timing.TriggerGroupsId;
            // job.TriggerJobId client tarafından dolu geliyor 
            var addJobResult = database.Job.Add(job.Id, job);

            if (!addJobResult)
            {
                triggeredJob.Timing.TriggerGroupsId = beforeTriggerGroupsId;
                triggeredJob.Timing.IsTriggerMain = beforeIsTriggerMain;
                triggeredJob.Timing.IsTrigger = beforeIsTrigger;

                var triggeredJobRollbackResult = database.Job.Update(triggeredJob.Id, triggeredJob);
                if (!triggeredJobRollbackResult)
                {
                    // todo error log
                    // burada triggered job bozuldu ama geri alınamadı. Buraya senaryo düşün
                    response.Message = "Job eklenemedi, işlemler geri alınamadı."; // todo statuscode
                }
                else
                {
                    response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                }

                response.IsSucces = false;
                return response;
            }

            response.IsSucces = true;
            return response;
        }
    }
}