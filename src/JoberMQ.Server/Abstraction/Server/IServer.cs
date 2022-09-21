using JoberMQ.Server.Abstraction.Config;

namespace JoberMQ.Server.Abstraction.Server
{
    public interface IServer
    {
        public IServerConfig ServerConfig { get; }
        internal IStatusCode StatusCode { get; }
        public void Start();
    }
}
