using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Abstraction.Timing;

namespace JoberMQ.Implementation.Timing
{
    internal abstract class TimingBase : ITiming
    {
        public abstract Task<ResponseModel> Timing(JobDbo job);
    }
}
