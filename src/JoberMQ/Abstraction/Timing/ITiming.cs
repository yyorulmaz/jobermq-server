using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Abstraction.Timing
{
    internal interface ITiming
    {
        public Task<ResponseModel> Timing(JobDbo job);
    }
}
