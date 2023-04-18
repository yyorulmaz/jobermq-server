using JoberMQ.Broker.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Enums.Timing;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Implementation.Default;

namespace JoberMQ.Timing.Factories
{
    internal class TimingFactory
    {
        internal static ITiming CreateTiming(TimingFactoryEnum timingFactory, TimingTypeEnum timingType, IMessageBroker messageBroker, IDatabase database, ISchedule schedule, IStatusCode statusCode)
        {
            ITiming timing;

            switch (timingFactory)
            {
                case TimingFactoryEnum.Default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(messageBroker, database, statusCode);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(messageBroker, database, schedule, statusCode);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTimingTrigger(database, statusCode);
                            break;
                        default:
                            throw new System.Exception(nameof(TimingTypeEnum) + " none");
                    }
                    break;
                default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DfTimingNow(messageBroker, database, statusCode);
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DfTimingSchedule(messageBroker, database, schedule, statusCode);
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DfTimingTrigger(database, statusCode);
                            break;
                        default:
                            throw new System.Exception(nameof(TimingTypeEnum) + " none");
                    }
                    break;
            }

            return timing;
        }
    }
}
