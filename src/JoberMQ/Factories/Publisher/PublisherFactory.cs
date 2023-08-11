using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Abstraction.Publisher;
using JoberMQ.Implementation.Publisher.Default;

namespace JoberMQ.Factories.Publisher
{
    internal class PublisherFactory
    {
        internal static IPublisher Create(PublisherFactoryEnum publisherFactory, PublisherTypeEnum publisherType)
        {
            IPublisher publisher;

            switch (publisherFactory)
            {
                case PublisherFactoryEnum.Default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DefaultPublisherStandart();
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DefaultPublisherEmbed();
                            break;
                        default:
                            throw new System.Exception(nameof(PublisherTypeEnum) + " none");
                    }
                    break;
                default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DefaultPublisherStandart();
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DefaultPublisherEmbed();
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
