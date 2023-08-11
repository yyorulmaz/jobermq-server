using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Abstraction.Publisher
{
    internal interface IPublisher
    {
        internal Task<ResponseModel> Publish(JobDbo job);
    }
}
