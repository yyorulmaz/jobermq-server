using TimerFramework;
using JoberMQ.Abstraction.Timing;

namespace JoberMQ.Implementation.Timing
{
    internal abstract class ScheduleBase : ISchedule
    {
        protected ITimer jobTimer;
        public ITimer JobTimer { get => jobTimer; set => jobTimer = value; }
        public abstract bool Start();
        public abstract void Action(TimerModel timer);
    }
}
