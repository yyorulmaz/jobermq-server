using JoberMQ.Common.Enums.Timing;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Implementation.Default;

namespace JoberMQ.Timing.Factories
{
    internal class ScheduleFactory
    {
        internal static ISchedule CreateSchedule(ScheduleFactoryEnum scheduleFactory, IDatabase database)
        {
            ISchedule schedule;

            switch (scheduleFactory)
            {
                case ScheduleFactoryEnum.Default:
                    schedule = new DfSchedule(database);
                    break;
                default:
                    schedule = new DfSchedule(database);
                    break;
            }

            return schedule;
        }
    }
}
