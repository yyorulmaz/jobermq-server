using JoberMQ.Common.Enums.Timing;
using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Implementation.Default;

namespace JoberMQ.Timing.Factories
{
    internal class ScheduleFactory
    {
        internal static ISchedule CreateSchedule(ScheduleFactoryEnum scheduleFactory, IDatabaseService databaseService)
        {
            ISchedule schedule;

            switch (scheduleFactory)
            {
                case ScheduleFactoryEnum.Default:
                    schedule = new DfSchedule(databaseService);
                    break;
                default:
                    schedule = new DfSchedule(databaseService);
                    break;
            }

            return schedule;
        }
    }
}
