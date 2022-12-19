using JoberMQ.Business.Abstraction.Configuration;
using JoberMQ.Business.Constants;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Business.Implementation.Configuration
{
    internal class DfConfigurationData : IConfigurationData
    {
        DbMemFactoryEnum dbMemFactory = DefaultDataConst.DbMemFactory;
        public DbMemFactoryEnum DbMemFactory { get => dbMemFactory; set => dbMemFactory = value; }


        DbMemDataFactoryEnum dbMemDataFactory = DefaultDataConst.DbMemDataFactory;
        public DbMemDataFactoryEnum DbMemDataFactory { get => dbMemDataFactory; set => dbMemDataFactory = value; }


        DbTextFactoryEnum dbTextFactory = DefaultDataConst.DbTextFactory;
        public DbTextFactoryEnum DbTextFactory { get => dbTextFactory; set => dbTextFactory = value; }
        ConcurrentDictionary<string, DbTextFileConfigModel> dbTextFileConfigDatas = DefaultDataConst.DbTextFileConfigDatas;
        public ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas { get => dbTextFileConfigDatas; set => dbTextFileConfigDatas = value; }


        DbOprFactoryEnum dbOprFactory = DefaultDataConst.DbOprFactory;
        public DbOprFactoryEnum DbOprFactory { get => dbOprFactory; set => dbOprFactory = value; }


        DboCreatorFactoryEnum dboCreatorFactory = DefaultDataConst.DboCreatorFactory;
        public DboCreatorFactoryEnum DboCreatorFactory { get => dboCreatorFactory; set => dboCreatorFactory = value; }


        DbOprServiceFactoryEnum dbOprServiceFactory = DefaultDataConst.DbOprServiceFactory;
        public DbOprServiceFactoryEnum DbOprServiceFactory { get => dbOprServiceFactory; set => dbOprServiceFactory = value; }


        string completedDataRemovesTimer = DefaultDataConst.CompletedDataRemovesTimer;
        public string CompletedDataRemovesTimer { get => completedDataRemovesTimer; set => completedDataRemovesTimer = value; }
    }
}
