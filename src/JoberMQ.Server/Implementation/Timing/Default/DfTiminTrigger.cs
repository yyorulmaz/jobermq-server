using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;

namespace JoberMQ.Server.Implementation.Timing.Default
{
    internal class DfTiminTrigger : TimingBase
    {
        public DfTiminTrigger(IDbOprService dbOprService) : base(dbOprService)
        {
        }

        public override JobDataAddResponseModel Timing(JobDataDbo jobData)
        {
            var response = new JobDataAddResponseModel();
            response.IsOnline = true;
            response.JobId = jobData.Id;

            var triggeredJobData = dbOprService.JobData.Get(jobData.TriggerJobId.Value);
            var beforeTriggerGroupsId = triggeredJobData.TriggerGroupsId;
            var beforeIsTriggerMain = triggeredJobData.IsTriggerMain;
            var beforeIsTrigger = triggeredJobData.IsTrigger;

            if (triggeredJobData.TriggerJobId == null)
            {
                triggeredJobData.TriggerGroupsId = triggeredJobData.Id;
                triggeredJobData.IsTriggerMain = true;
            }
            triggeredJobData.IsTrigger = true;

            var triggeredJobDataResult = dbOprService.JobData.Update(triggeredJobData);
            if (!triggeredJobDataResult)
            {
                response.IsSuccess = false;
                response.Message = "Triggerred JobData güncellenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            jobData.TriggerGroupsId = triggeredJobData.TriggerGroupsId;
            // jobData.TriggerJobId client tarafından dolu geliyor 
            var addJobDataResult = dbOprService.JobData.Add(jobData);

            if (!addJobDataResult)
            {
                triggeredJobData.TriggerGroupsId = beforeTriggerGroupsId;
                triggeredJobData.IsTriggerMain = beforeIsTriggerMain;
                triggeredJobData.IsTrigger = beforeIsTrigger;

                var triggeredJobDataRollbackResult = dbOprService.JobData.Update(triggeredJobData);
                if (!triggeredJobDataRollbackResult)
                {
                    // todo error log
                    // burada triggered job bozuldu ama geri alınamadı. Buraya senaryo düşün
                    response.Message = "JobData eklenemedi, işlemler geri alınamadı."; // todo statuscode
                }
                else
                {
                    response.Message = "JobData eklenemedi, işlemler geri alındı."; // todo statuscode
                }

                response.IsSuccess = false;
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}