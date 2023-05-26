using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models;
using JoberMQ.Common.Models.Response;
using System.Threading.Tasks;

namespace JoberMQ.Publisher.Abstraction
{
    internal interface IPublisher
    {
        public Task<ResponseModel> Publish(JobDbo job);
    }
}
