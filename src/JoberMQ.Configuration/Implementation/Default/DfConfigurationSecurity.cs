using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationSecurity : IConfigurationSecurity
    {
        string securityKey = DefaultSecurityConst.SecurityKey;
        public string SecurityKey { get => securityKey; set => securityKey = value; }
    }
}
