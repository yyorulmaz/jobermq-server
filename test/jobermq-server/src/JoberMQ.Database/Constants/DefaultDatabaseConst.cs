using JoberMQ.Entities.Enums.Configuration;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Database.Constants
{
    internal class DefaultDatabaseConst
    {
        internal const ConfigurationDatabaseFactoryEnum ConfigurationDatabaseFactory = ConfigurationDatabaseFactoryEnum.Default;

        internal const DbMemFactoryEnum DbMemFactory = DbMemFactoryEnum.Default;
        internal const DbMemDataFactoryEnum DbMemDataFactory = DbMemDataFactoryEnum.Data;

        internal const DbTextFactoryEnum DbTextFactory = DbTextFactoryEnum.Default;
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

            clientDatas.TryAdd("JobTransaction", new DbTextFileConfigModel
            {
                DbPath = dbPath,
                DbFolderPath = "JobTransaction",
                DbFileName = "JobTransaction",
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

        internal const DbOprFactoryEnum DbOprFactory = DbOprFactoryEnum.Default;

        internal const DboCreatorFactoryEnum DboCreatorFactory = DboCreatorFactoryEnum.Default;

        internal const DatabaseServiceFactoryEnum DatabaseServiceFactory = DatabaseServiceFactoryEnum.Default;

        internal const string CompletedDataRemovesTimer = "0 */5 * ? * *";
    }
}
