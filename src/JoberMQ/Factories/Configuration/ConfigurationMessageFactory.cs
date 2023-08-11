using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationMessageFactory
    {
        internal static IConfigurationMessage Create(ConfigurationMessageFactoryEnum configurationMessageFactory)
        {
            IConfigurationMessage result;

            switch (configurationMessageFactory)
            {
                case ConfigurationMessageFactoryEnum.Default:
                    result = new DefaultConfigurationMessage();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
