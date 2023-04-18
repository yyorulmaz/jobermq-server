using JoberMQ.Library.Enums.Publisher;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationPublisher
    {
        public PublisherFactoryEnum PublisherFactory { get; set; }
    }
}
