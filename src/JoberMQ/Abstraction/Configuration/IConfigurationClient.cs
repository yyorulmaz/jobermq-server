using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationClient
    {
        public ClientsFactoryEnum ClientsFactory { get; set; }
        public ClientFactoryEnum ClientFactory { get; set; }
    }
}
