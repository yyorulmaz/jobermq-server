using JoberMQ.Common.Enums.Timing;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Constants;

namespace JoberMQ.Timing.Implementation.Default
{
    internal class DfConfigurationTiming : IConfigurationTiming
    {

        ScheduleFactoryEnum scheduleFactory = DefaultTimingConst.ScheduleFactory;
        public ScheduleFactoryEnum ScheduleFactory { get => scheduleFactory; set => scheduleFactory = value; }
        TimingFactoryEnum timingFactory = DefaultTimingConst.TimingFactory;
        public TimingFactoryEnum TimingFactory { get => timingFactory; set => timingFactory = value; }
    }
}
