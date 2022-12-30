using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Distributor.Abstraction
{
    public interface IConfigurationDistributor
    {
        public DistributorFactoryEnum DistributorFactory { get; set; }
        public MemFactoryEnum DistributorMemFactory { get; set; }
        public MemDataFactoryEnum DistributorMemDataFactory { get; set; }
        public ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas { get; set; }

    }
}
