using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Timing.Abstraction
{
    internal interface ITiming
    {
        public JobAddResponseModel Timing(JobDbo job);
    }
}
