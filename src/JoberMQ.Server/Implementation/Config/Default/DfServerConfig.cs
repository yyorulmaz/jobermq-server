using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Factories.Config;

namespace JoberMQ.Server.Implementation.Config.Default
{
    internal class DfServerConfig: IServerConfig
    {
        ServerFactoryEnum IServerConfig.ServerFactory => ServerConst.ServerFactory;

        IStatusCodeConfig statusCode;
        public IStatusCodeConfig StatusCode => statusCode;

        ISecurityConfig security;
        public ISecurityConfig Security => security;

        public DfServerConfig()
        {
            statusCode = StatusCodeConfigFactory.CreateStatusCodeConfig(ServerConst.Config.StatusCodeConfigFactory);
            security = SecurityConfigFactory.CreateSecurityConfig(ServerConst.Config.SecurityConfigFactory);
        }
    }
}
