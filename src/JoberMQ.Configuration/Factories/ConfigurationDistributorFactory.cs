using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Library.Enums.Configuration;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationDistributorFactory
    {
        internal static IConfigurationDistributor Create(ConfigurationDistributorFactoryEnum configurationDistributorFactory)
        {
            IConfigurationDistributor configurationDistributor;

            switch (configurationDistributorFactory)
            {
                case ConfigurationDistributorFactoryEnum.Default:
                    configurationDistributor = new DfConfigurationDistributor();
                    break;
                default:
                    configurationDistributor = new DfConfigurationDistributor();
                    break;
            }

            return configurationDistributor;
        }
    }
}
