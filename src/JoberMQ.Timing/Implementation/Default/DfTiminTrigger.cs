using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Database.Abstraction.DbService;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfTiminTrigger : TimingBase
    {
        public DfTiminTrigger(IDatabase database) : base(database)
        {
        }

        public override JobAddResponseModel Timing(JobDbo job)
        {
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

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
                response.IsSuccess = false;
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

                response.IsSuccess = false;
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}