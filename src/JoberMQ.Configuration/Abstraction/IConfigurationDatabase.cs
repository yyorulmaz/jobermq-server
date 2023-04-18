using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Database.Models;
using JoberMQ.Library.Enums.Database;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Abstraction
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
