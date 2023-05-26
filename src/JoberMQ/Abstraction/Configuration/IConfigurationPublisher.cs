using JoberMQ.Common.Enums.Publisher;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationPublisher
    {
        public PublisherFactoryEnum PublisherFactory { get; set; }
    }
}
