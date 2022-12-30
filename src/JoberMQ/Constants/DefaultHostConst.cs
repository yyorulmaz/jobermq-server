using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Constants
{
    internal class DefaultHostConst
    {
        internal const ConfigurationHostFactoryEnum ConfigurationHostFactory = ConfigurationHostFactoryEnum.Default;
        internal const string HostName = "localhost";
        internal const int Port = 7654;
        internal const int PortSsl = 7655;
    }
}
