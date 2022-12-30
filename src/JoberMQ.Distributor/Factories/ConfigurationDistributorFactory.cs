using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Implementation.Default;

namespace JoberMQ.Distributor.Factories
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
