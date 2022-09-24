using TimerFramework;

namespace JoberMQ.Server.Abstraction.Schedule
{
    internal interface ISchedule
    {
        public ITimer JobDataTimer { get; set; }
        public bool Start();
    }
}
