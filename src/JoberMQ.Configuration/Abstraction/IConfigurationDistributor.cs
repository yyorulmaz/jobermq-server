using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Models.Configuration;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationDistributor
    {
        public DistributorFactoryEnum DistributorFactory { get; set; }

        public MemFactoryEnum DistributorsMemFactory { get; set; }
        public MemDataFactoryEnum DistributorsMemDataFactory { get; set; }
        public MemFactoryEnum DistributorMemFactory { get; set; }
        public MemDataFactoryEnum DistributorMemDataFactory { get; set; }
        public ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas { get; set; }

    }
}
