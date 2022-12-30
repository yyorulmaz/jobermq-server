using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Abstraction.Configuration
{
    public interface IConfigurationDatabase
    {
        public DbMemFactoryEnum DbMemFactory { get; set; }
        public DbMemDataFactoryEnum DbMemDataFactory { get; set; }

        public DbTextFactoryEnum DbTextFactory { get; set; }
        public ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas { get; set; }

        public DbOprFactoryEnum DbOprFactory { get; set; }

        public DboCreatorFactoryEnum DboCreatorFactory { get; set; }

        public DatabaseServiceFactoryEnum DatabaseServiceFactory { get; set; }

        public string CompletedDataRemovesTimer { get; set; }
    }
}
