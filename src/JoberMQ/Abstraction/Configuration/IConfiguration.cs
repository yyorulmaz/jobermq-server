namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfiguration
    {
        public bool IsOwinHost { get; set; }
        public IConfigurationClient ConfigurationClient { get; set; }
        public IConfigurationStatusCode ConfigurationStatusCode { get; set; }
        public IConfigurationPublisher ConfigurationPublisher { get; set; }
        public IConfigurationTiming ConfigurationTiming { get; set; }
        public IConfigurationQueue ConfigurationQueue { get; set; }
        public IConfigurationMessage ConfigurationMessage { get; set; }
        public IConfigurationDatabase ConfigurationDatabase { get; set; }
        public IConfigurationHost ConfigurationHost { get; set; }
        public IConfigurationSecurity ConfigurationSecurity { get; set; }
        public IConfigurationDistributor ConfigurationDistributor { get; set; }
        public IConfigurationBroker ConfigurationBroker { get; set; }
    }
}
