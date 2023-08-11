using JoberMQ.Common.Enums.Host;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationHost : IConfigurationHost
    {
        HostFactoryEnum hostFactory = ConfigurationHostConst.HostFactory;
        public HostFactoryEnum HostFactory { get => hostFactory; set => hostFactory = value; }

        string hostName = ConfigurationHostConst.HostName;
        public string HostName { get => hostName; set => hostName = value; }
        int port = ConfigurationHostConst.Port;
        public int Port { get => port; set => port = value; }
        int portSsl = ConfigurationHostConst.PortSsl;
        public int PortSsl { get => portSsl; set => portSsl = value; }
    }
}
