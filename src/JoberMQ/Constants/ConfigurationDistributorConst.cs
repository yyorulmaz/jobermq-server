using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Configuration;

namespace JoberMQ.Constants
{
    internal class ConfigurationDistributorConst
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

            clientDatas.TryAdd("def.dis.direct", new DefaultDistributorConfigModel
            {
                DistributorKey = "def.dis.direct",
                DistributorType = DistributorTypeEnum.Direct,
                DistributorSearchSourceType = DistributorSearchSourceTypeEnum.None,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true,
                IsDefault = true
            });

            clientDatas.TryAdd("def.dis.search.queuekey", new DefaultDistributorConfigModel
            {
                DistributorKey = "def.dis.search.queuekey",
                DistributorType = DistributorTypeEnum.Search,
                DistributorSearchSourceType = DistributorSearchSourceTypeEnum.QueueKey,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true,
                IsDefault = true
            });

            clientDatas.TryAdd("def.dis.search.queuetag", new DefaultDistributorConfigModel
            {
                DistributorKey = "def.dis.search.queuetag",
                DistributorType = DistributorTypeEnum.Search,
                DistributorSearchSourceType = DistributorSearchSourceTypeEnum.QueueTag,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true,
                IsDefault = true
            });

            return clientDatas;
        }
    }
}
