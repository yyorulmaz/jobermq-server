﻿using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Library.Enums.Configuration;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationDatabaseFactory
    {
        internal static IConfigurationDatabase Create(ConfigurationDatabaseFactoryEnum configurationDatabaseFactory)
        {
            IConfigurationDatabase configurationDatabase;

            switch (configurationDatabaseFactory)
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
