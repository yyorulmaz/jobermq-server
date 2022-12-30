using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DbOpr;

namespace JoberMQ.Server.Implementation.Timing.Default
{
    internal class DfTiminTrigger : TimingBase
    {
        public DfTiminTrigger(IDbOprService dbOprService) : base(dbOprService)
        {
        }

        public override JobAddResponseModel Timing(JobDbo job)
        {
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

            var triggeredJob = dbOprService.Job.Get(job.TriggerJobId.Value);
            var beforeTriggerGroupsId = triggeredJob.TriggerGroupsId;
            var beforeIsTriggerMain = triggeredJob.IsTriggerMain;
            var beforeIsTrigger = triggeredJob.IsTrigger;

            if (triggeredJob.TriggerJobId == null)
            {
                triggeredJob.TriggerGroupsId = triggeredJob.Id;
                triggeredJob.IsTriggerMain = true;
            }
            triggeredJob.IsTrigger = true;

            var triggeredJobResult = dbOprService.Job.Update(triggeredJob);
            if (!triggeredJobResult)
            {
                response.IsSuccess = false;
                response.Message = "Triggerred Job güncellenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            job.TriggerGroupsId = triggeredJob.TriggerGroupsId;
            // job.TriggerJobId client tarafından dolu geliyor 
            var addJobResult = dbOprService.Job.Add(job);

            if (!addJobResult)
            {
                triggeredJob.TriggerGroupsId = beforeTriggerGroupsId;
                triggeredJob.IsTriggerMain = beforeIsTriggerMain;
                triggeredJob.IsTrigger = beforeIsTrigger;

                var triggeredJobRollbackResult = dbOprService.Job.Update(triggeredJob);
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