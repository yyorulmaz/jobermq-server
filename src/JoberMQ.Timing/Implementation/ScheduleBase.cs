using JoberMQ.Database.Abstraction;
using JoberMQ.Timing.Abstraction;
using TimerFramework;

namespace JoberMQ.Timing.Implementation
{
    internal abstract class ScheduleBase : ISchedule
    {
        protected ITimer jobTimer;
        protected readonly IDatabase database;

        public ScheduleBase(IDatabase database)
        {
            this.database = database;
        }

        public ITimer JobTimer { get => jobTimer; set => jobTimer = value; }
        public abstract bool Start();
        public abstract void Action(TimerModel timer);
    }
}
