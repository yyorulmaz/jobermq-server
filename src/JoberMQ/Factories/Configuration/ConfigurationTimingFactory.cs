using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationTimingFactory
    {
        internal static IConfigurationTiming Create(ConfigurationTimingFactoryEnum configurationTimingFactory)
        {
            IConfigurationTiming result;

            switch (configurationTimingFactory)
            {
                case ConfigurationTimingFactoryEnum.Default:
                    result = new DefaultConfigurationTiming();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
