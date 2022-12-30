using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Implementation.Default;

namespace JoberMQ.Timing.Factories
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
