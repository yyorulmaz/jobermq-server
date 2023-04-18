using JoberMQ.Library.Enums.Timing;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationTiming
    {
        public ScheduleFactoryEnum ScheduleFactory { get; set; }
        public TimingFactoryEnum TimingFactory { get; set; }
    }
}
