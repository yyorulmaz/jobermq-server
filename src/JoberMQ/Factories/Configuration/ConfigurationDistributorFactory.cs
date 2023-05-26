using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationDistributorFactory
    {
        internal static IConfigurationDistributor Create(ConfigurationDistributorFactoryEnum configurationDistributorFactory)
        {
            IConfigurationDistributor configurationDistributor;

            switch (configurationDistributorFactory)
            {
                case ConfigurationDistributorFactoryEnum.Default:
                    configurationDistributor = new DefaultConfigurationDistributor();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return configurationDistributor;
        }
    }
}
