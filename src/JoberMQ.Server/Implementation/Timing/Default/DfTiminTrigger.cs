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

            var triggeredJobData = dbOprService.JobData.Get(jobData.TriggerJobId.Value);

            if (triggeredJobData.TriggerJobId == null)
            {
                triggeredJobData.TriggerGroupsId = triggeredJobData.Id;
                triggeredJobData.IsTriggerMain = true;
            }

            triggeredJobData.IsTrigger = true;
            dbOprService.JobData.Update(triggeredJobData);

            jobData.TriggerGroupsId = triggeredJobData.TriggerGroupsId;
            dbOprService.JobData.Add(jobData);

            response.IsSuccess = true;
            response.JobId = jobData.Id;
            return response;
        }
    }
}
