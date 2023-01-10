using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Config;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Database.Enums;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationDistributor : IConfigurationDistributor
    {














        DistributorFactoryEnum distributorFactory = DefaultDistributorConst.DistributorFactory;
        public DistributorFactoryEnum DistributorFactory { get => distributorFactory; set => distributorFactory = value; }


        MemFactoryEnum distributorsMemFactory = DefaultDistributorConst.DistributorsMemFactory;
        public MemFactoryEnum DistributorsMemFactory { get => distributorsMemFactory; set => distributorsMemFactory = value; }
        MemDataFactoryEnum distributorsMemDataFactory = DefaultDistributorConst.DistributorsMemDataFactory;
        public MemDataFactoryEnum DistributorsMemDataFactory { get => distributorsMemDataFactory; set => distributorsMemDataFactory = value; }


        MemFactoryEnum distributorMemFactory = DefaultDistributorConst.DistributorMemFactory;
        public MemFactoryEnum DistributorMemFactory { get => distributorMemFactory; set => distributorMemFactory = value; }
        MemDataFactoryEnum distributorMemDataFactory = DefaultDistributorConst.DistributorMemDataFactory;
        public MemDataFactoryEnum DistributorMemDataFactory { get => distributorMemDataFactory; set => distributorMemDataFactory = value; }

        ConcurrentDictionary<string, DefaultDistributorConfigModel> defaultDistributorConfigDatas = DefaultDistributorConst.DefaultDistributorConfigDatas;
        public ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas { get => defaultDistributorConfigDatas; set => defaultDistributorConfigDatas = value; }
    }
}
