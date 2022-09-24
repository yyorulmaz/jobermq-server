using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;

namespace JoberMQ.Server.Abstraction.Publisher
{
    internal interface IPublisher
    {
        public JobDataAddResponseModel Publish(JobDataDbo jobData);
    }
}
