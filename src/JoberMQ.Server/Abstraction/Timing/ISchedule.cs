using TimerFramework;

namespace JoberMQ.Server.Abstraction.Timing
{
    internal interface ISchedule
    {
        public ITimer JobDataTimer { get; set; }
        public bool Start();
    }
}
