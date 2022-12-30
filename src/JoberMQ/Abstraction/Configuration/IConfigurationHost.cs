namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationHost
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public int PortSsl { get; set; }
    }
}
