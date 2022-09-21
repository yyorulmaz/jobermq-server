using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.Server;
using JoberMQ.Server.Factories.Server;

namespace JoberMQ.Server
{
    internal class Factory
    {
        internal static IServer Server { get; set; }
        public static IServer CreateServer(ServerConfigModel serverConfig) => Server = ServerFactory.CreateServer(serverConfig);
    }
}
