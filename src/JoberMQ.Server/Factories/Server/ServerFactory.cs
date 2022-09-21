using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.Server;
using JoberMQ.Server.Implementation.Server.Default;

namespace JoberMQ.Server.Factories.Server
{
    internal class ServerFactory
    {
        public static IServer CreateServer(ServerConfigModel serverConfig)
        {
            IServer server;
            switch (serverConfig.ServerFactory)
            {
                case ServerFactoryEnum.Default:
                    server = new DfServer(serverConfig);
                    break;
                default:
                    server = new DfServer(serverConfig);
                    break;
            }

            return server;
        }
    }
}
