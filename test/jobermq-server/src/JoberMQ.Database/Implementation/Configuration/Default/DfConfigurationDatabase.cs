using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Constants;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.Configuration.Default
{
    internal class DfConfigurationDatabase : IConfigurationDatabase
    {
        DbMemFactoryEnum dbMemFactory = DefaultDatabaseConst.DbMemFactory;
        public DbMemFactoryEnum DbMemFactory { get => dbMemFactory; set => dbMemFactory = value; }


        DbMemDataFactoryEnum dbMemDataFactory = DefaultDatabaseConst.DbMemDataFactory;
        public DbMemDataFactoryEnum DbMemDataFactory { get => dbMemDataFactory; set => dbMemDataFactory = value; }


        DbTextFactoryEnum dbTextFactory = DefaultDatabaseConst.DbTextFactory;
        public DbTextFactoryEnum DbTextFactory { get => dbTextFactory; set => dbTextFactory = value; }
        ConcurrentDictionary<string, DbTextFileConfigModel> dbTextFileConfigDatas = DefaultDatabaseConst.DbTextFileConfigDatas;
        public ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas { get => dbTextFileConfigDatas; set => dbTextFileConfigDatas = value; }


        DbOprFactoryEnum dbOprFactory = DefaultDatabaseConst.DbOprFactory;
        public DbOprFactoryEnum DbOprFactory { get => dbOprFactory; set => dbOprFactory = value; }


        DboCreatorFactoryEnum dboCreatorFactory = DefaultDatabaseConst.DboCreatorFactory;
        public DboCreatorFactoryEnum DboCreatorFactory { get => dboCreatorFactory; set => dboCreatorFactory = value; }


        DatabaseServiceFactoryEnum databaseServiceFactory = DefaultDatabaseConst.DatabaseServiceFactory;
        public DatabaseServiceFactoryEnum DatabaseServiceFactory { get => databaseServiceFactory; set => databaseServiceFactory = value; }


        string completedDataRemovesTimer = DefaultDatabaseConst.CompletedDataRemovesTimer;
        public string CompletedDataRemovesTimer { get => completedDataRemovesTimer; set => completedDataRemovesTimer = value; }
    }
}
