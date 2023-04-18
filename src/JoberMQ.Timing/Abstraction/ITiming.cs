using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Response;
using System.Threading.Tasks;

namespace JoberMQ.Timing.Abstraction
{
    internal interface ITiming
    {
        public Task<ResponseModel> Timing(JobDbo job);
    }
}
