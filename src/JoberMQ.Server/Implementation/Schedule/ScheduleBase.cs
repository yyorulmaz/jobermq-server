using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Schedule;
using TimerFramework;

namespace JoberMQ.Server.Implementation.Schedule
{
    internal abstract class ScheduleBase: ISchedule
    {
        protected ITimer jobDataTimer;
        protected readonly IDbOprService dbOprService;
        protected readonly IDboCreator dboCreator;

        public ScheduleBase(IDbOprService dbOprService, IDboCreator dboCreator)
        {
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
        }

        public ITimer JobDataTimer { get => jobDataTimer; set => jobDataTimer = value; }
        public abstract bool Start();
        public abstract void Action(TimerModel timer);
    }
}
