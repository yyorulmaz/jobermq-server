using JoberMQ.Broker.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Enums.Publisher;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Publisher.Abstraction;
using JoberMQ.Publisher.Implementation.Default;
using JoberMQ.Timing.Abstraction;

namespace JoberMQ.Publisher.Factories
{
    internal class PublisherFactory
    {
        internal static IPublisher Create(PublisherFactoryEnum publisherFactory, PublisherTypeEnum publisherType, IConfiguration configuration, IDatabase database, ISchedule schedule, IMessageBroker messageBroker, IStatusCode statusCode)
        {
            IPublisher publisher;

            switch (publisherFactory)
            {
                case PublisherFactoryEnum.Default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DfPublisherStandart(configuration, database, schedule, messageBroker, statusCode);
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DfPublisherEmbed(configuration, database, statusCode);
                            break;
                        default:
                            throw new System.Exception(nameof(PublisherTypeEnum) + " none");
                    }
                    break;
                default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DfPublisherStandart(configuration, database, schedule, messageBroker, statusCode);
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DfPublisherEmbed(configuration, database, statusCode);
                            break;
                        default:
                            throw new System.Exception(nameof(PublisherTypeEnum) + " none");
                    }
                    break;
            }

            return publisher;
        }
    }
}
