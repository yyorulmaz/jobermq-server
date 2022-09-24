using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;
using JoberMQ.Entities.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Entities.Constants
{
    internal class ServerConst
    {
        internal const ServerFactoryEnum ServerFactory = ServerFactoryEnum.Default;
        internal const ClientFactoryEnum ClientFactory = ClientFactoryEnum.Default;
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
            internal const DbOprFactoryEnum DbOprFactory = DbOprFactoryEnum.Default;
            internal const DbOprServiceFactoryEnum DbOprServiceFactory = DbOprServiceFactoryEnum.Default;
            internal const DbMemFactoryEnum DbMemFactory = DbMemFactoryEnum.Default;
            internal const DbMemDataFactoryEnum DbMemDataFactory = DbMemDataFactoryEnum.Data;
            internal const DbTextFactoryEnum DbTextFactory = DbTextFactoryEnum.Default;
            internal const DboCreatorFactoryEnum DboCreatorFactory = DboCreatorFactoryEnum.Default;

            internal const string CompletedDataRemovesTimer = "0 */5 * ? * *";


            internal static ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas = DbTextFileConfigData();
            private static ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigData()
            {
                var clientDatas = new ConcurrentDictionary<string, DbTextFileConfigModel>();

                var dbPath = "Database";
                var dbFileSeparator = '.';
                var dbArchiveFileSeparator = '_';
                var dbFileExtension = "txt";
                var maxRowCount = 100000;

                clientDatas.TryAdd("User", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "User",
                    DbFileName = "User",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("Distributor", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "Distributor",
                    DbFileName = "Distributor",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("Queue", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "Queue",
                    DbFileName = "Queue",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("EventSub", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "EventSub",
                    DbFileName = "EventSub",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("JobData", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "JobData",
                    DbFileName = "JobData",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("Job", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "Job",
                    DbFileName = "Job",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("Message", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "Message",
                    DbFileName = "Message",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                clientDatas.TryAdd("MessageResult", new DbTextFileConfigModel
                {
                    DbPath = dbPath,
                    DbFolderPath = "MessageResult",
                    DbFileName = "MessageResult",
                    DbFileSeparator = dbFileSeparator,
                    DbArchiveFileSeparator = dbArchiveFileSeparator,
                    DbFileExtension = dbFileExtension,
                    MaxRowCount = maxRowCount
                });

                return clientDatas;
            }
        }
        internal class Broker
        {
            internal const QueueFactoryEnum QueueFactory = QueueFactoryEnum.Default;
            internal const QueueChildPriorityFactoryEnum QueueChildPriorityFactory = QueueChildPriorityFactoryEnum.Default;
            internal const QueueChildFIFOFactoryEnum QueueChildFIFOFactory = QueueChildFIFOFactoryEnum.Default;
            internal const QueueChildLIFOFactoryEnum QueueChildLIFOFactory = QueueChildLIFOFactoryEnum.Default;
            internal const DistributorFactoryEnum DistributorFactory = DistributorFactoryEnum.Default;
            
        }
        internal class Hosting
        {
            internal const string HostName = "localhost";
            internal const int Port = 7654;
            internal const int PortSsl = 7655;
        }
    }
}
