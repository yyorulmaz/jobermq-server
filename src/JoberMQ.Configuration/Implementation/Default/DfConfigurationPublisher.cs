using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Enums.Publisher;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationPublisher : IConfigurationPublisher
    {
        PublisherFactoryEnum publisherFactory = DefaultPublisherConst.PublisherFactory;
        public PublisherFactoryEnum PublisherFactory { get => publisherFactory; set => publisherFactory = value; }
    }
}
