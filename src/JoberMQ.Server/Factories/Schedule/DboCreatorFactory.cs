using JoberMQ.Entities.Enums.Schedule;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Schedule;
using JoberMQ.Server.Implementation.Schedule.Default;

namespace JoberMQ.Server.Factories.Schedule
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
