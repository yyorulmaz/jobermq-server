using JoberMQ.Common.Enums.Jober;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationJober
    {
        public JoberFactoryEnum JoberFactory { get; set; }
        public bool IsJoberActive { get; set; }
    }
}
