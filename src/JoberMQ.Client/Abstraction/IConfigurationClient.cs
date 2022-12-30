using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Abstraction
{
    public interface IConfigurationClient
    {
        public ClientFactoryEnum ClientFactory { get; set; }
        public ClientGroupFactoryEnum ClientGroupFactory { get; set; }
        public ClientServiceFactoryEnum ClientServiceFactory { get; set; }
    }
}