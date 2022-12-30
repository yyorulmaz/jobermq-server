using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Models;
using JoberMQ.Common.Enums.Database;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Abstraction.Configuration
{
    public interface IConfigurationDatabase
    {
        public MemFactoryEnum DbMemFactory { get; set; }
        public MemDataFactoryEnum DbMemDataFactory { get; set; }

        public TextFactoryEnum DbTextFactory { get; set; }
        public ConcurrentDictionary<string, TextFileConfigModel> DbTextFileConfigDatas { get; set; }

        public OprFactoryEnum DbOprFactory { get; set; }

        public DboCreatorFactoryEnum DboCreatorFactory { get; set; }

        public DatabaseServiceFactoryEnum DatabaseServiceFactory { get; set; }

        public string CompletedDataRemovesTimer { get; set; }
    }
}
