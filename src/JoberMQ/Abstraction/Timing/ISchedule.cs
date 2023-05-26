using System.Threading.Tasks;
using TimerFramework;

namespace JoberMQ.Abstraction.Timing
{
    internal interface ISchedule
    {
        public ITimer JobTimer { get; set; }
        public bool Start();
    }
}
