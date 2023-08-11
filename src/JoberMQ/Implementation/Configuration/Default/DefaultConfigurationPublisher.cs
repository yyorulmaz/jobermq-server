using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationPublisher : IConfigurationPublisher
    {
        PublisherFactoryEnum publisherFactory = ConfigurationPublisherConst.PublisherFactory;
        public PublisherFactoryEnum PublisherFactory { get => publisherFactory; set => publisherFactory = value; }
    }
}
