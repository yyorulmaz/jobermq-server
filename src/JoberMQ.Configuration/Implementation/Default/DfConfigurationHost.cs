using JoberMQ.Common.Enums.Host;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationHost : IConfigurationHost
    {
        HostFactoryEnum hostFactory = DefaultHostConst.HostFactory;
        public HostFactoryEnum HostFactory { get => hostFactory; set => hostFactory = value; }

        string hostName = DefaultHostConst.HostName;
        public string HostName { get => hostName; set => hostName = value; }
        int port = DefaultHostConst.Port;
        public int Port { get => port; set => port = value; }
        int portSsl = DefaultHostConst.PortSsl;
        public int PortSsl { get => portSsl; set => portSsl = value; }
    }
}
