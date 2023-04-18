using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Enums.Timing;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationTiming : IConfigurationTiming
    {
        ScheduleFactoryEnum scheduleFactory = DefaultTimingConst.ScheduleFactory;
        public ScheduleFactoryEnum ScheduleFactory { get => scheduleFactory; set => scheduleFactory = value; }
        TimingFactoryEnum timingFactory = DefaultTimingConst.TimingFactory;
        public TimingFactoryEnum TimingFactory { get => timingFactory; set => timingFactory = value; }
    }
}
