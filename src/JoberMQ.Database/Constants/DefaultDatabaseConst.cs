using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Models;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Common.Enums.Database;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Constants
{
    internal class DefaultDatabaseConst
    {
        internal const MemFactoryEnum DbMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum DbMemDataFactory = MemDataFactoryEnum.Data;

        internal const TextFactoryEnum DbTextFactory = TextFactoryEnum.Default;
        internal static ConcurrentDictionary<string, TextFileConfigModel> DbTextFileConfigDatas = DbTextFileConfigData();
        private static ConcurrentDictionary<string, TextFileConfigModel> DbTextFileConfigData()
        {
            var clientDatas = new ConcurrentDictionary<string, TextFileConfigModel>();

            var dbPath = "Database";
            var dbFileSeparator = '.';
            var dbArchiveFileSeparator = '_';
            var dbFileExtension = "txt";
            var maxRowCount = 100000;

            clientDatas.TryAdd("User", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "User",
                DbFileName = "User",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("Distributor", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "Distributor",
                DbFileName = "Distributor",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("Queue", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "Queue",
                DbFileName = "Queue",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("EventSub", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "EventSub",
                DbFileName = "EventSub",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("Job", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "Job",
                DbFileName = "Job",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("JobTransaction", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "JobTransaction",
                DbFileName = "JobTransaction",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("Message", new TextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "Message",
                DbFileName = "Message",
                DbFileSeparator = dbFileSeparator,
                DbArchiveFileSeparator = dbArchiveFileSeparator,
                DbFileExtension = dbFileExtension,
                MaxRowCount = maxRowCount
            });

            clientDatas.TryAdd("MessageResult", new TextFileConfigModel
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

        internal const OprFactoryEnum DbOprFactory = OprFactoryEnum.Default;

        internal const DboCreatorFactoryEnum DboCreatorFactory = DboCreatorFactoryEnum.Default;

        internal const DatabaseServiceFactoryEnum DatabaseServiceFactory = DatabaseServiceFactoryEnum.Default;

        internal const string CompletedDataRemovesTimer = "0 */5 * ? * *";
    }
}
