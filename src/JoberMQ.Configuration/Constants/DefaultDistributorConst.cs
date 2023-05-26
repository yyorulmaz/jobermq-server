using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Configuration;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultDistributorConst
    {











        internal const DistributorFactoryEnum DistributorFactory = DistributorFactoryEnum.Default;

        internal const MemFactoryEnum DistributorsMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum DistributorsMemDataFactory = MemDataFactoryEnum.None;

        internal const MemFactoryEnum DistributorMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum DistributorMemDataFactory = MemDataFactoryEnum.None;

        //internal const string DefaultDistributorDirectKey = "distributor.default.direct";
        internal static ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas = DefaultDistributorConfigData();
        private static ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigData()
        {
            var clientDatas = new ConcurrentDictionary<string, DefaultDistributorConfigModel>();

            clientDatas.TryAdd("default.distributor.direct", new DefaultDistributorConfigModel
            {
                DistributorKey = "default.distributor.direct",
                DistributorType = DistributorTypeEnum.Direct,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            clientDatas.TryAdd("default.distributor.event", new DefaultDistributorConfigModel
            {
                DistributorKey = "default.distributor.event",
                DistributorType = DistributorTypeEnum.Event,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            clientDatas.TryAdd("default.distributor.filter", new DefaultDistributorConfigModel
            {
                DistributorKey = "default.distributor.filter",
                DistributorType = DistributorTypeEnum.Filter,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            return clientDatas;
        }
    }
}
