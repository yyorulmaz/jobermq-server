using JoberMQ.Entities.Enums.Broker;
using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Enums.Publisher;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Enums.Schedule;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;
using JoberMQ.Entities.Enums.Timing;
using JoberMQ.Entities.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Entities.Constants
{
    internal class ServerConst
    {
        internal const ServerFactoryEnum ServerFactory = ServerFactoryEnum.Default;
        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
        internal const ClientGroupFactoryEnum ClientGroupFactory = ClientGroupFactoryEnum.Default;
        internal const ClientServiceFactoryEnum ClientServiceFactory = ClientServiceFactoryEnum.Default;

        internal class StatusCode
        {
            internal const StatusCodeMessageLanguageEnum StatusCodeMessageLanguage = StatusCodeMessageLanguageEnum.en;
        }
        internal class Security
        {
            internal const string SecurityKey = "böyle_bir_aşk_görülmemiş_dünyada_ne_geçmişte_nede_bundan_sonrada";
        }
        internal class DbOpr
        {
            internal const DboCreatorFactoryEnum DboCreatorFactory = DboCreatorFactoryEnum.Default;
        }
        internal class Broker
        {
            internal const BrokerFactoryEnum BrokerFactory = BrokerFactoryEnum.Default;
            internal const QueueFactoryEnum QueueFactory = QueueFactoryEnum.Default;
            internal const QueueChildPriorityFactoryEnum QueueChildPriorityFactory = QueueChildPriorityFactoryEnum.Default;
            internal const QueueChildFIFOFactoryEnum QueueChildFIFOFactory = QueueChildFIFOFactoryEnum.Default;
            internal const QueueChildLIFOFactoryEnum QueueChildLIFOFactory = QueueChildLIFOFactoryEnum.Default;
            internal const DistributorFactoryEnum DistributorFactory = DistributorFactoryEnum.Default;


            internal const string DefaultDistributorDirectKey = "dis.default.direct";
            internal static ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas = DefaultDistributorConfigData();
            private static ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigData()
            {
                var clientDatas = new ConcurrentDictionary<string, DefaultDistributorConfigModel>();

                clientDatas.TryAdd("dis.default.direct", new DefaultDistributorConfigModel
                {
                    DistributorKey = "dis.default.direct",
                    DistributorType = DistributorTypeEnum.Direct,
                    PermissionType = PermissionTypeEnum.All,
                    IsDurable = true
                });

                return clientDatas;
            }


            internal const string DefaultQueueSpecialKey = "queue.default.special";
            internal static ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigDatas = DefaultQueueConfigData();
            private static ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigData()
            {
                var clientDatas = new ConcurrentDictionary<string, DefaultQueueConfigModel>();

                clientDatas.TryAdd("queue.default.special", new DefaultQueueConfigModel
                {
                    QueueKey = "queue.default.special",
                    MatchType = MatchTypeEnum.Special,
                    SendType = SendTypeEnum.FIFO,
                    PermissionType = PermissionTypeEnum.All,
                    IsDurable = true
                });

                return clientDatas;
            }
        }
        internal class Hosting
        {
            internal const string HostName = "localhost";
            internal const int Port = 7654;
            internal const int PortSsl = 7655;
        }
        internal class Timing
        {
            internal const ScheduleFactoryEnum ScheduleFactory = ScheduleFactoryEnum.Default;
            internal const TimingFactoryEnum TimingFactory = TimingFactoryEnum.Default;
        }
        internal class Publisher
        {
            internal const PublisherFactoryEnum PublisherFactory = PublisherFactoryEnum.Default;
        }
        
    }
}
