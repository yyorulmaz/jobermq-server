using JoberMQ.Entities.Enums.Publisher;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Publisher;
using JoberMQ.Server.Abstraction.Timing;
using JoberMQ.Server.Implementation.Publisher.Default;

namespace JoberMQ.Server.Factories.Publisher
{
    internal class PublisherFactory
    {
        internal static IPublisher CreatePublisher(ServerConfigModel serverConfig, PublisherTypeEnum publisherType, IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            IPublisher publisher;

            switch (serverConfig.PublisherConfig.PublisherFactory)
            {
                case PublisherFactoryEnum.Default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DfPublisherStandart(serverConfig,dbOprService, dboCreator, schedule);
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DfPublisherEmbed(dbOprService);
                            break;
                        default:
                            publisher = new DfPublisherStandart(serverConfig,dbOprService, dboCreator, schedule);
                            break;
                    }
                    break;
                default:
                    switch (publisherType)
                    {
                        case PublisherTypeEnum.Standart:
                            publisher = new DfPublisherStandart(serverConfig,dbOprService, dboCreator, schedule);
                            break;
                        case PublisherTypeEnum.Embed:
                            publisher = new DfPublisherEmbed(dbOprService);
                            break;
                        default:
                            publisher = new DfPublisherStandart(serverConfig, dbOprService, dboCreator, schedule);
                            break;
                    }
                    break;
            }

            return publisher;
        }
    }
}
