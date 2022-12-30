using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;
using TimerFramework;

namespace JoberMQ.Timing.Implementation
{
    internal abstract class ScheduleBase : ISchedule
    {
        protected ITimer jobTimer;
        protected readonly IDatabaseService databaseService;

        public ScheduleBase(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ITimer JobTimer { get => jobTimer; set => jobTimer = value; }
        public abstract bool Start();
        public abstract void Action(TimerModel timer);
    }
}
