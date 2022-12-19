using JoberMQ.Business.Abstraction.Configuration;
using JoberMQ.Business.Implementation.Configuration;
using JoberMQ.Entities.Enums.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Business.Factories.Configuration
{
    internal class ConfigurationFactory
    {
        internal static IConfiguration CreateConfiguration(ConfigurationFactoryEnum factory)
        {
            IConfiguration configuration;

            switch (factory)
            {
                case ConfigurationFactoryEnum.Default:
                    configuration = new DfConfiguration();
                    break;
                default:
                    configuration = new DfConfiguration();
                    break;
            }

            return configuration;
        }

        internal static IConfigurationData CreateConfigurationData(
            ConfigurationDataFactoryEnum configurationDataFactory)
        {
            IConfigurationData configurationData;

            switch (configurationDataFactory)
            {
                case ConfigurationDataFactoryEnum.Default:
                    configurationData = new DfConfigurationData();
                    break;
                default:
                    configurationData = new DfConfigurationData();
                    break;
            }

            return configurationData;
        }
    }
}
