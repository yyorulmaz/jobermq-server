using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationStatusCodeFactory
    {
        public static IConfigurationStatusCode Create(ConfigurationStatusCodeFactoryEnum factory)
        {
            IConfigurationStatusCode configurationStatusCode;

            switch (factory)
            {
                case ConfigurationStatusCodeFactoryEnum.Default:
                    configurationStatusCode = new DfConfigurationStatusCode();
                    break;
                default:
                    configurationStatusCode = new DfConfigurationStatusCode();
                    break;
            }

            return configurationStatusCode;
        }
    }
}
