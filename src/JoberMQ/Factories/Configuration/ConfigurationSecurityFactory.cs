using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationSecurityFactory
    {
        public static IConfigurationSecurity Create(ConfigurationSecurityFactoryEnum configurationSecurityFactory)
        {
            IConfigurationSecurity configurationSecurity;

            switch (configurationSecurityFactory)
            {
                case ConfigurationSecurityFactoryEnum.Default:
                    configurationSecurity = new DefaultConfigurationSecurity();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return configurationSecurity;
        }
    }
}
