using JoberMQ.Entities.Enums.Schedule;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;
using JoberMQ.Server.Implementation.Timing.Default;

namespace JoberMQ.Server.Factories.Timing
{
    internal class ScheduleFactory
    {
        internal static ISchedule CreateSchedule(ScheduleFactoryEnum scheduleFactory, IDbOprService dbOprService, IDboCreator dboCreator)
        {
            ISchedule schedule;

            switch (scheduleFactory)
            {
                case ScheduleFactoryEnum.Default:
                    schedule = new DfSchedule(dbOprService, dboCreator);
                    break;
                default:
                    schedule = new DfSchedule(dbOprService, dboCreator);
                    break;
            }

            return schedule;
        }
    }
}
