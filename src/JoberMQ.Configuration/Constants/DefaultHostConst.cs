using JoberMQ.Library.Enums.Host;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultHostConst
    {
        internal const HostFactoryEnum HostFactory = HostFactoryEnum.Default;
        internal const string HostName = "localhost";
        internal const int Port = 7654;
        internal const int PortSsl = 7655;
    }
}
