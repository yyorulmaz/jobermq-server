using TimerFramework;

namespace JoberMQ.Server.Abstraction.Timing
{
    internal interface ISchedule
    {
        public ITimer JobTimer { get; set; }
        public bool Start();
    }
}
