using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationSecurityFactory
    {
        public static IConfigurationSecurity Create(ConfigurationSecurityFactoryEnum configurationSecurityFactory)
        {
            IConfigurationSecurity configurationSecurity;

            switch (configurationSecurityFactory)
            {
                case ConfigurationSecurityFactoryEnum.Default:
                    configurationSecurity = new DfConfigurationSecurity();
                    break;
                default:
                    configurationSecurity = new DfConfigurationSecurity();
                    break;
            }

            return configurationSecurity;
        }
    }
}
