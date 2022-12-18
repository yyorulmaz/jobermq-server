using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;

namespace JoberMQ.Server.Abstraction.Publisher
{
    internal interface IPublisher
    {
        public JobAddResponseModel Publish(JobDbo job);
    }
}
