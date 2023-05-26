using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Configuration;

namespace JoberMQ.Abstraction.Configuration
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
