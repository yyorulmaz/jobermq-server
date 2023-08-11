using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationSecurity : IConfigurationSecurity
    {
        string securityKey = ConfigurationSecurityConst.SecurityKey;
        public string SecurityKey { get => securityKey; set => securityKey = value; }
    }
}
