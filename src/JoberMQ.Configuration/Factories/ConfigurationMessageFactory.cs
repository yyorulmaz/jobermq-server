using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationMessageFactory
    {
        internal static IConfigurationMessage Create(ConfigurationMessageFactoryEnum configurationMessageFactory)
        {
            IConfigurationMessage configurationMessage;

            switch (configurationMessageFactory)
            {
                case ConfigurationMessageFactoryEnum.Default:
                    configurationMessage = new DfConfigurationMessage();
                    break;
                default:
                    configurationMessage = new DfConfigurationMessage();
                    break;
            }

            return configurationMessage;
        }
    }
}