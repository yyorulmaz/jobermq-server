using TimerFramework;

namespace JoberMQ.Timing.Abstraction
{
    internal interface ISchedule
    {
        public ITimer JobTimer { get; set; }
        public bool Start();
    }
}
