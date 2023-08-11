using JoberMQ.Common.Enums.Timing;
using JoberMQ.Abstraction.Timing;
using JoberMQ.Implementation.Timing.Default;

namespace JoberMQ.Factories.Timing
{
    internal class TimingFactory
    {
        internal static ITiming CreateTiming(TimingFactoryEnum timingFactory, TimingTypeEnum timingType)
        {
            ITiming timing;

            switch (timingFactory)
            {
                case TimingFactoryEnum.Default:
                    switch (timingType)
                    {
                        case TimingTypeEnum.Now:
                            timing = new DefaultTimingNow();
                            break;
                        case TimingTypeEnum.Schedule:
                            timing = new DefaultTimingSchedule();
                            break;
                        case TimingTypeEnum.Trigger:
                            timing = new DefaultTimingTrigger();
                            break;
                        default:
                            throw new System.Exception(nameof(TimingTypeEnum) + " none");
                    }
                    break;
                default:
                    throw new System.Exception("none");
            }

            return timing;
        }
    }
}
