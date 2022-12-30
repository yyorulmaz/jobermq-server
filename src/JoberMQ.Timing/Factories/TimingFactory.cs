using JoberMQ.Broker.Abstraction;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Implementation.Default;

namespace JoberMQ.Timing.Factories
{
    internal class TimingFactory
    {
        internal static ITiming CreateTiming(TimingFactoryEnum timingFactory, TimingTypeEnum timingType, IMessageBroker messageBroker, IDatabaseService databaseService, ISchedule schedule)
        {
            ITiming timing;

            switch (timingFactory)
            {
                case TimingFactoryEnum.Default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(messageBroker, databaseService);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(messageBroker, databaseService, schedule);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTiminTrigger(databaseService);
                            break;
                        default:
                            timing = new DfTiminTrigger(databaseService);
                            break;
                    }
                    break;
                default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(messageBroker, databaseService);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(messageBroker, databaseService, schedule);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTiminTrigger(databaseService);
                            break;
                        default:
                            timing = new DfTiminTrigger(databaseService);
                            break;
                    }
                    break;
            }

            return timing;
        }
    }
}
