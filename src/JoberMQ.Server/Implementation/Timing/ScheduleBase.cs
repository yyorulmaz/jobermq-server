﻿using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;
using TimerFramework;

namespace JoberMQ.Server.Implementation.Timing
{
    internal abstract class ScheduleBase : ISchedule
    {
        protected ITimer jobTimer;
        protected readonly IDbOprService dbOprService;
        protected readonly IDboCreator dboCreator;

        public ScheduleBase(IDbOprService dbOprService, IDboCreator dboCreator)
        {
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
        }

        public ITimer JobTimer { get => jobTimer; set => jobTimer = value; }
        public abstract bool Start();
        public abstract void Action(TimerModel timer);
    }
}
