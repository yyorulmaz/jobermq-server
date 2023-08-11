using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationDistributor : IConfigurationDistributor
    {
        
        DistributorFactoryEnum distributorFactory = ConfigurationDistributorConst.DistributorFactory;
        public DistributorFactoryEnum DistributorFactory { get => distributorFactory; set => distributorFactory = value; }


        MemFactoryEnum distributorsMemFactory = ConfigurationDistributorConst.DistributorsMemFactory;
        public MemFactoryEnum DistributorsMemFactory { get => distributorsMemFactory; set => distributorsMemFactory = value; }
        MemDataFactoryEnum distributorsMemDataFactory = ConfigurationDistributorConst.DistributorsMemDataFactory;
        public MemDataFactoryEnum DistributorsMemDataFactory { get => distributorsMemDataFactory; set => distributorsMemDataFactory = value; }


        MemFactoryEnum distributorMemFactory = ConfigurationDistributorConst.DistributorMemFactory;
        public MemFactoryEnum DistributorMemFactory { get => distributorMemFactory; set => distributorMemFactory = value; }
        MemDataFactoryEnum distributorMemDataFactory = ConfigurationDistributorConst.DistributorMemDataFactory;
        public MemDataFactoryEnum DistributorMemDataFactory { get => distributorMemDataFactory; set => distributorMemDataFactory = value; }

        ConcurrentDictionary<string, DefaultDistributorConfigModel> defaultDistributorConfigDatas = ConfigurationDistributorConst.DefaultDistributorConfigDatas;
        public ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas { get => defaultDistributorConfigDatas; set => defaultDistributorConfigDatas = value; }
    }
}
