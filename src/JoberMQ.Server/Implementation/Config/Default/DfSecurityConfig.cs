using JoberMQ.Entities.Constants;
using JoberMQ.Server.Abstraction.Config;
using System;

namespace JoberMQ.Server.Implementation.Config.Default
{
    internal class DfSecurityConfig : ISecurityConfig
    {
        string securityKey = ServerConst.Config.SecurityKey;
        public string SecurityKey => securityKey;

        public bool SetSecurityConfig(string securityKey)
        {
            try
            {
                this.securityKey = securityKey;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
