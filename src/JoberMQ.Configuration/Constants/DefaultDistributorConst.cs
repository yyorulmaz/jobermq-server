using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Config;
using JoberMQ.Library.Database.Enums;
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

        internal const string DefaultDistributorDirectKey = "dis.default.direct";
        internal static ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas = DefaultDistributorConfigData();
        private static ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigData()
        {
            var clientDatas = new ConcurrentDictionary<string, DefaultDistributorConfigModel>();

            clientDatas.TryAdd("dis.default.direct", new DefaultDistributorConfigModel
            {
                DistributorKey = "dis.default.direct",
                DistributorType = DistributorTypeEnum.Direct,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            return clientDatas;
        }
    }
}
