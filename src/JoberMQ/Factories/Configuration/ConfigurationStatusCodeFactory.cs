using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationStatusCodeFactory
    {
        internal static IConfigurationStatusCode Create(ConfigurationStatusCodeFactoryEnum configurationStatusCodeFactory)
        {
            IConfigurationStatusCode result;

            switch (configurationStatusCodeFactory)
            {
                case ConfigurationStatusCodeFactoryEnum.Default:
                    result = new DefaultConfigurationStatusCode();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
