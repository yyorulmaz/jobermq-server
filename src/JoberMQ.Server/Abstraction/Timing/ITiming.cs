using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;

namespace JoberMQ.Server.Abstraction.Timing
{
    internal interface ITiming
    {
        public JobDataAddResponseModel Timing(JobDataDbo jobData);
    }
}
