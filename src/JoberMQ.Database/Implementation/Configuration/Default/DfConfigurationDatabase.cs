using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Models;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Constants;
using JoberMQ.Common.Enums.Database;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.Configuration.Default
{
    internal class DfConfigurationDatabase : IConfigurationDatabase
    {
        MemFactoryEnum dbMemFactory = DefaultDatabaseConst.DbMemFactory;
        public MemFactoryEnum DbMemFactory { get => dbMemFactory; set => dbMemFactory = value; }


        MemDataFactoryEnum dbMemDataFactory = DefaultDatabaseConst.DbMemDataFactory;
        public MemDataFactoryEnum DbMemDataFactory { get => dbMemDataFactory; set => dbMemDataFactory = value; }


        TextFactoryEnum dbTextFactory = DefaultDatabaseConst.DbTextFactory;
        public TextFactoryEnum DbTextFactory { get => dbTextFactory; set => dbTextFactory = value; }
        ConcurrentDictionary<string, TextFileConfigModel> dbTextFileConfigDatas = DefaultDatabaseConst.DbTextFileConfigDatas;
        public ConcurrentDictionary<string, TextFileConfigModel> DbTextFileConfigDatas { get => dbTextFileConfigDatas; set => dbTextFileConfigDatas = value; }


        OprFactoryEnum dbOprFactory = DefaultDatabaseConst.DbOprFactory;
        public OprFactoryEnum DbOprFactory { get => dbOprFactory; set => dbOprFactory = value; }


        DboCreatorFactoryEnum dboCreatorFactory = DefaultDatabaseConst.DboCreatorFactory;
        public DboCreatorFactoryEnum DboCreatorFactory { get => dboCreatorFactory; set => dboCreatorFactory = value; }


        DatabaseServiceFactoryEnum databaseServiceFactory = DefaultDatabaseConst.DatabaseServiceFactory;
        public DatabaseServiceFactoryEnum DatabaseServiceFactory { get => databaseServiceFactory; set => databaseServiceFactory = value; }


        string completedDataRemovesTimer = DefaultDatabaseConst.CompletedDataRemovesTimer;
        public string CompletedDataRemovesTimer { get => completedDataRemovesTimer; set => completedDataRemovesTimer = value; }
    }
}
