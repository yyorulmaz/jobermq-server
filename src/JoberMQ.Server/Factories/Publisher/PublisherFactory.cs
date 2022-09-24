using JoberMQ.Entities.Enums.Publisher;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Publisher;
using JoberMQ.Server.Abstraction.Schedule;
using JoberMQ.Server.Implementation.Publisher.Default;

namespace JoberMQ.Server.Factories.Publisher
{
    internal class PublisherFactory
    {
        internal static IPublisher CreatePublisher(PublisherFactoryEnum publisherFactory, PublisherTypeEnum publisherType, IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            IPublisher publisher;

            switch (publisherFactory)
            {
                case PublisherFactoryEnum.Default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DfPublisherStandart(dbOprService, dboCreator, schedule);
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DfPublisherEmbed(dbOprService);
                            break;
                        default:
                            publisher = new DfPublisherStandart(dbOprService, dboCreator, schedule);
                            break;
                    }
                    break;
                default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DfPublisherStandart(dbOprService, dboCreator, schedule);
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DfPublisherEmbed(dbOprService);
                            break;
                        default:
                            publisher = new DfPublisherStandart(dbOprService, dboCreator, schedule);
                            break;
                    }
                    break;
            }

            return publisher;
        }
    }
}
