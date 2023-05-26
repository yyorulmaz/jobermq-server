using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Models;
using JoberMQ.Common.Enums.Database;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationDatabase : IConfigurationDatabase
    {
        MemFactoryEnum dbMemFactory = ConfigurationDatabaseConst.DbMemFactory;
        public MemFactoryEnum DbMemFactory { get => dbMemFactory; set => dbMemFactory = value; }


        MemDataFactoryEnum dbMemDataFactory = ConfigurationDatabaseConst.DbMemDataFactory;
        public MemDataFactoryEnum DbMemDataFactory { get => dbMemDataFactory; set => dbMemDataFactory = value; }


        TextFactoryEnum dbTextFactory = ConfigurationDatabaseConst.DbTextFactory;
        public TextFactoryEnum DbTextFactory { get => dbTextFactory; set => dbTextFactory = value; }
        ConcurrentDictionary<string, TextFileConfigModel> dbTextFileConfigDatas = ConfigurationDatabaseConst.DbTextFileConfigDatas;
        public ConcurrentDictionary<string, TextFileConfigModel> DbTextFileConfigDatas { get => dbTextFileConfigDatas; set => dbTextFileConfigDatas = value; }


        OprFactoryEnum dbOprFactory = ConfigurationDatabaseConst.DbOprFactory;
        public OprFactoryEnum DbOprFactory { get => dbOprFactory; set => dbOprFactory = value; }


        DboCreatorFactoryEnum dboCreatorFactory = ConfigurationDatabaseConst.DboCreatorFactory;
        public DboCreatorFactoryEnum DboCreatorFactory { get => dboCreatorFactory; set => dboCreatorFactory = value; }


        DatabaseServiceFactoryEnum databaseServiceFactory = ConfigurationDatabaseConst.DatabaseServiceFactory;
        public DatabaseServiceFactoryEnum DatabaseServiceFactory { get => databaseServiceFactory; set => databaseServiceFactory = value; }


        string completedDataRemovesTimer = ConfigurationDatabaseConst.CompletedDataRemovesTimer;
        public string CompletedDataRemovesTimer { get => completedDataRemovesTimer; set => completedDataRemovesTimer = value; }
    }
}
