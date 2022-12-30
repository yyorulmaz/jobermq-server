using JoberMQ.Abstraction.Configuration;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    public class ConfigurationFactory
    {
        public static IConfiguration CreateConfiguration(ConfigurationFactoryEnum factory)
        {
            IConfiguration configuration;

            switch (factory)
            {
                case ConfigurationFactoryEnum.Default:
                    configuration = new DfConfiguration();
                    break;
                default:
                    configuration = new DfConfiguration();
                    break;
            }

            return configuration;
        }
    }
}
