using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Implementation.Timing.Default
{
    internal class DefaultTimingTrigger : TimingBase
    {
        public override async Task<ResponseModel> Timing(JobDbo job)
        {
            var response = new ResponseModel();
            response.IsOnline = true;
            response.Id = job.Id;

            var triggeredJob = JoberHost.JoberMQ.Database.Job.Get(job.Timing.TriggerJobId.Value);

            var beforeTriggerGroupsId = triggeredJob.Timing.TriggerGroupsId;
            var beforeIsTriggerMain = triggeredJob.Timing.IsTriggerMain;
            var beforeIsTrigger = triggeredJob.Timing.IsTrigger;

            if (triggeredJob.Timing.TriggerJobId == null)
            {
                triggeredJob.Timing.TriggerGroupsId = triggeredJob.Id;
                triggeredJob.Timing.IsTriggerMain = true;
            }
            triggeredJob.Timing.IsTrigger = true;

            var triggeredJobResult = JoberHost.JoberMQ.Database.Job.Update(triggeredJob.Id, triggeredJob);
            if (!triggeredJobResult)
            {
                response.IsSucces = false;
                response.Message = "Triggerred Job güncellenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            job.Timing.TriggerGroupsId = triggeredJob.Timing.TriggerGroupsId;
            // job.TriggerJobId client tarafından dolu geliyor 
            var addJobResult = JoberHost.JoberMQ.Database.Job.Add(job.Id, job);

            if (!addJobResult)
            {
                triggeredJob.Timing.TriggerGroupsId = beforeTriggerGroupsId;
                triggeredJob.Timing.IsTriggerMain = beforeIsTriggerMain;
                triggeredJob.Timing.IsTrigger = beforeIsTrigger;

                var triggeredJobRollbackResult = JoberHost.JoberMQ.Database.Job.Update(triggeredJob.Id, triggeredJob);
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
