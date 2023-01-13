using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Config;
using JoberMQ.Library.Database.Enums;
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
