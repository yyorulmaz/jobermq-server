using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DfConfigurationHost : IConfigurationHost
    {
        string hostName = DefaultHostConst.HostName;
        public string HostName { get => hostName; set => hostName = value; }
        int port = DefaultHostConst.Port;
        public int Port { get => port; set => port = value; }
        int portSsl = DefaultHostConst.PortSsl;
        public int PortSsl { get => portSsl; set => portSsl = value; }
    }
}
