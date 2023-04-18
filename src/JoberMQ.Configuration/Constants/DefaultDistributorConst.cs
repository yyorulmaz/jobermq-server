using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Models.Configuration;
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

            clientDatas.TryAdd("distributor.default.direct.special", new DefaultDistributorConfigModel
            {
                DistributorKey = "distributor.default.direct.special",
                DistributorType = DistributorTypeEnum.Direct,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            clientDatas.TryAdd("distributor.default.direct.group", new DefaultDistributorConfigModel
            {
                DistributorKey = "distributor.default.direct.group",
                DistributorType = DistributorTypeEnum.Direct,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            clientDatas.TryAdd("distributor.default.direct.queue", new DefaultDistributorConfigModel
            {
                DistributorKey = "distributor.default.direct.queue",
                DistributorType = DistributorTypeEnum.Direct,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            clientDatas.TryAdd("distributor.default.event", new DefaultDistributorConfigModel
            {
                DistributorKey = "distributor.default.event",
                DistributorType = DistributorTypeEnum.Event,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            return clientDatas;
        }
    }
}
