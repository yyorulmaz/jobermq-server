using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Business.Abstraction.Configuration
{
    internal interface IConfigurationData
    {
        public DbMemFactoryEnum DbMemFactory { get; set; }
        public DbMemDataFactoryEnum DbMemDataFactory { get; set; }

        public DbTextFactoryEnum DbTextFactory { get; set; }
        public ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas { get; set; }

        public DbOprFactoryEnum DbOprFactory { get; set; }

        public DboCreatorFactoryEnum DboCreatorFactory { get; set; }

        public DbOprServiceFactoryEnum DbOprServiceFactory { get; set; }

        public string CompletedDataRemovesTimer { get; set; }

    }
}
