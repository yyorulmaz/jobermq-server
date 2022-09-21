using JoberMQ.Entities.Enums.Config;
using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Implementation.Config.Default;

namespace JoberMQ.Server.Factories.Config
{
    internal class ServerConfigFactory
    {
        public static IServerConfig CreateServerConfig(ServerConfigFactoryEnum serverConfigFactory)
        {
            IServerConfig serverConfig;

            switch (serverConfigFactory)
            {
                case ServerConfigFactoryEnum.Default:
                    serverConfig = new DfServerConfig();
                    break;
                default:
                    serverConfig = new DfServerConfig();
                    break;
            }

            return serverConfig;
        }
    }
}
