using JoberMQ.Entities.Constants;
using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Abstraction.Server;
using JoberMQ.Server.Factories.Config;
using JoberMQ.Server.Factories.Server;

namespace JoberMQ.Server
{
    internal class Factory
    {
        public static IServerConfig CreateServerConfig()
        {
            var serverConfigFactory = ServerConst.Config.ServerConfigFactory;
            return ServerConfigFactory.CreateServerConfig(serverConfigFactory);
        }
        public static IServer CreateServer(IServerConfig serverConfig) => ServerFactory.CreateServer(serverConfig);
    }
}
