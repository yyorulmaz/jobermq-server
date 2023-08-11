using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Abstraction.Publisher;

namespace JoberMQ.Implementation.Publisher
{
    internal abstract class PublisherBase : IPublisher
    {
        public abstract Task<ResponseModel> Publish(JobDbo job);
    }
}
