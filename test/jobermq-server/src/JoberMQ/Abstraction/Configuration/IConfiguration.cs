using JoberMQ.Database.Abstraction.Configuration;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfiguration
    {
        public IConfigurationDatabase ConfigurationDatabase { get; set; }
    }
}
