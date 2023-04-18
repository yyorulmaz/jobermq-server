using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Response;
using System.Threading.Tasks;

namespace JoberMQ.Publisher.Abstraction
{
    internal interface IPublisher
    {
        public Task<ResponseModel> Publish(JobDbo job);
    }
}
