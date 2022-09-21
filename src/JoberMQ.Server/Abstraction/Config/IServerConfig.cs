using JoberMQ.Entities.Enums.Server;

namespace JoberMQ.Server.Abstraction.Config
{
    public interface IServerConfig
    {
        internal ServerFactoryEnum ServerFactory { get; }

        public IStatusCodeConfig StatusCode { get; }
        public ISecurityConfig Security { get; }
    }
}
