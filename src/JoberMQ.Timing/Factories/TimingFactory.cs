using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Implementation.Default;

namespace JoberMQ.Timing.Factories
{
    internal class TimingFactory
    {
        internal static ITiming CreateTiming(TimingFactoryEnum timingFactory, TimingTypeEnum timingType, IMessageBroker messageBroker, IDatabase database, ISchedule schedule)
        {
            ITiming timing;

            switch (timingFactory)
            {
                case TimingFactoryEnum.Default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(messageBroker, database);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(messageBroker, database, schedule);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTiminTrigger(database);
                            break;
                        default:
                            timing = new DfTiminTrigger(database);
                            break;
                    }
                    break;
                default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(messageBroker, database);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(messageBroker, database, schedule);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTiminTrigger(database);
                            break;
                        default:
                            timing = new DfTiminTrigger(database);
                            break;
                    }
                    break;
            }

            return timing;
        }
    }
}
