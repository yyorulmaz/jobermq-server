using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Config;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Constants;
using System.Collections.Concurrent;

namespace JoberMQ.Distributor.Implementation.Default
{
    internal class DfConfigurationDistributor : IConfigurationDistributor
    {
        DistributorFactoryEnum distributorFactory = DefaultDistributorConst.DistributorFactory;
        public DistributorFactoryEnum DistributorFactory { get => distributorFactory; set => distributorFactory = value; }
        MemFactoryEnum distributorMemFactory = DefaultDistributorConst.DistributorMemFactory;
        public MemFactoryEnum DistributorMemFactory { get => distributorMemFactory; set => distributorMemFactory = value; }
        MemDataFactoryEnum distributorMemDataFactory = DefaultDistributorConst.DistributorMemDataFactory;
        public MemDataFactoryEnum DistributorMemDataFactory { get => distributorMemDataFactory; set => distributorMemDataFactory = value; }

        ConcurrentDictionary<string, DefaultDistributorConfigModel> defaultDistributorConfigDatas = DefaultDistributorConst.DefaultDistributorConfigDatas;
        public ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas { get => defaultDistributorConfigDatas; set => defaultDistributorConfigDatas = value; }
    }
}
