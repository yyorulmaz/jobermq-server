using JoberMQ.Common.Enums.Timing;
using JoberMQ.Abstraction.Timing;
using JoberMQ.Implementation.Timing.Default;

namespace JoberMQ.Factories.Timing
{
    internal class ScheduleFactory
    {
        internal static ISchedule CreateSchedule(ScheduleFactoryEnum scheduleFactory)
        {
            ISchedule schedule;

            switch (scheduleFactory)
            {
                case ScheduleFactoryEnum.Default:
                    schedule = new DefaultSchedule();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return schedule;
        }
    }
}
