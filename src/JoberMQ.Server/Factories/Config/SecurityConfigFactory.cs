using JoberMQ.Entities.Enums.Config;
using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Implementation.Config.Default;

namespace JoberMQ.Server.Factories.Config
{
    internal class SecurityConfigFactory
    {
        public static ISecurityConfig CreateSecurityConfig(SecurityConfigFactoryEnum securityConfigFactory)
        {
            ISecurityConfig securityConfig;

            switch (securityConfigFactory)
            {
                case SecurityConfigFactoryEnum.Default:
                    securityConfig = new DfSecurityConfig();
                    break;
                default:
                    securityConfig = new DfSecurityConfig();
                    break;
            }

            return securityConfig;
        }
    }
}
