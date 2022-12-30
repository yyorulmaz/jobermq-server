using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Implementation.Configuration.Default;
using JoberMQ.Entities.Enums.Configuration;

namespace JoberMQ.Database.Factories
{
    internal class ConfigurationDatabaseFactory
    {
        internal static IConfigurationDatabase CreateConfigurationData(
            ConfigurationDatabaseFactoryEnum configurationDataFactory)
        {
            IConfigurationDatabase configurationDatabase;

            switch (configurationDataFactory)
            {
                case ConfigurationDatabaseFactoryEnum.Default:
                    configurationDatabase = new DfConfigurationDatabase();
                    break;
                default:
                    configurationDatabase = new DfConfigurationDatabase();
                    break;
            }

            return configurationDatabase;
        }
    }
}
