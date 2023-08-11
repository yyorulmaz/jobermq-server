using JoberMQ.Common.Enums.Timing;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationTiming : IConfigurationTiming
    {
        ScheduleFactoryEnum scheduleFactory = ConfigurationTimingConst.ScheduleFactory;
        public ScheduleFactoryEnum ScheduleFactory { get => scheduleFactory; set => scheduleFactory = value; }
        TimingFactoryEnum timingFactory = ConfigurationTimingConst.TimingFactory;
        public TimingFactoryEnum TimingFactory { get => timingFactory; set => timingFactory = value; }
    }
}
