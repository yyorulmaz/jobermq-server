using JoberMQ.Common.Enums.Timing;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationTiming
    {
        public ScheduleFactoryEnum ScheduleFactory { get; set; }
        public TimingFactoryEnum TimingFactory { get; set; }
    }
}
