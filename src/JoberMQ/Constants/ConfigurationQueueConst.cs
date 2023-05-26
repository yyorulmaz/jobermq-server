using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Queue;

namespace JoberMQ.Constants
{
    internal class ConfigurationQueueConst
    {
        internal const QueueFactoryEnum QueueFactory = QueueFactoryEnum.Default;


        internal const MemFactoryEnum QueuesMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum QueuesMemDataFactory = MemDataFactoryEnum.None;


        internal const MemFactoryEnum QueueMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum QueueMemDataFactory = MemDataFactoryEnum.Data;


        internal const QueueMatchTypeFactoryEnum QueueMatchTypeFactory = QueueMatchTypeFactoryEnum.Default;
        internal const QueueOrderOfSendingTypeFactoryEnum QueueOrderOfSendingTypeFactory = QueueOrderOfSendingTypeFactoryEnum.Default;

        internal static ConcurrentDictionary<string, QueueModel> DefaultQueueConfigDatas = DefaultQueueConfigData();
        private static ConcurrentDictionary<string, QueueModel> DefaultQueueConfigData()
        {
            var clientDatas = new ConcurrentDictionary<string, QueueModel>();

            clientDatas.TryAdd("def.que.clientkey.free", new QueueModel
            {
                QueueKey = "def.que.clientkey.free",
                QueueMatchType = QueueMatchTypeEnum.ClientKey,
                QueueOrderOfSendingType = QueueOrderOfSendingTypeEnum.Free,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true,
                IsDefault = true
            });




            return clientDatas;
        }
    }
}
