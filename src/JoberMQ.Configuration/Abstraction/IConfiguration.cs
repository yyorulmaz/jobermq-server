namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfiguration
    {
        public IConfigurationJober ConfigurationJober { get; set; }
        public IConfigurationStatusCode ConfigurationStatusCode { get; set; }
        public IConfigurationClient ConfigurationClient { get; set; }
        public IConfigurationMessage ConfigurationMessage { get; set; }
        public IConfigurationDatabase ConfigurationDatabase { get; set; }
        public IConfigurationQueue ConfigurationQueue { get; set; }
        public IConfigurationDistributor ConfigurationDistributor { get; set; }
        public IConfigurationBroker ConfigurationBroker { get; set; }
        public IConfigurationSecurity ConfigurationSecurity { get; set; }
        public IConfigurationHost ConfigurationHost { get; set; }
        public IConfigurationTiming ConfigurationTiming { get; set; }
    }
}

