using JoberMQ.Common.Enums.Host;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationHost
    {
        public HostFactoryEnum HostFactory { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public int PortSsl { get; set; }
    }
}
