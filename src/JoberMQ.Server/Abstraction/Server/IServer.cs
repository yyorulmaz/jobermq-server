using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Abstraction.Server
{
    public interface IServer
    {
        internal bool IsServerActive { get; set; }

        public ServerConfigModel ServerConfig { get; }
        internal IStatusCode StatusCode { get; }
        internal IDbOprService DbOprService { get; }
        internal IClientService ClientService { get; }
        

        public void Start();
    }
}
