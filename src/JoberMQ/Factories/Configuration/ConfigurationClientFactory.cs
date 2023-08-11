using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationClientFactory
    {
        internal static IConfigurationClient Create(ConfigurationClientFactoryEnum configurationClientFactory)
        {
            IConfigurationClient result;

            switch (configurationClientFactory)
            {
                case ConfigurationClientFactoryEnum.Default:
                    result = new DefaultConfigurationClient();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
