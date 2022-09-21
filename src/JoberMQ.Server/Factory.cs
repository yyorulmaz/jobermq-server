using JoberMQ.Entities.Constants;
using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Factories.Config;

namespace JoberMQ.Server
{
    internal class Factory
    {
        public static IServerConfig CreateServerConfig()
        {
            var serverConfigFactory = ServerConst.Config.ServerConfigFactory;
            return ServerConfigFactory.CreateServerConfig(serverConfigFactory);
        }
    }
}
