using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.DbOpr;

namespace JoberMQ.Server.Abstraction.Server
{
    public interface IServer
    {
        internal bool IsServerActive { get; set; }

        public ServerConfigModel ServerConfig { get; }
        internal IStatusCode StatusCode { get; }
        internal IDbOprService DbOprService { get; }

        public void Start();
    }
}
