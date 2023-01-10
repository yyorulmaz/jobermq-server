using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationTimingFactory
    {
        internal static IConfigurationTiming Create(ConfigurationTimingFactoryEnum configurationTimingFactory)
        {
            IConfigurationTiming configurationTiming;

            switch (configurationTimingFactory)
            {
                case ConfigurationTimingFactoryEnum.Default:
                    configurationTiming = new DfConfigurationTiming();
                    break;
                default:
                    configurationTiming = new DfConfigurationTiming();
                    break;
            }

            return configurationTiming;
        }
    }
}
