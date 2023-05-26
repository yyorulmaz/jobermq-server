using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationDatabaseFactory
    {
        internal static IConfigurationDatabase Create(ConfigurationDatabaseFactoryEnum configurationDatabaseFactory)
        {
            IConfigurationDatabase configurationDatabase;

            switch (configurationDatabaseFactory)
            {
                case ConfigurationDatabaseFactoryEnum.Default:
                    configurationDatabase = new DefaultConfigurationDatabase();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return configurationDatabase;
        }
    }
}
