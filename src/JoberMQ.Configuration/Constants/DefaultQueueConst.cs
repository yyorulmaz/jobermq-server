using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Config;
using JoberMQ.Library.Database.Enums;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultQueueConst
    {
        internal const QueueFactoryEnum QueueFactory = QueueFactoryEnum.Default;


        internal const MemFactoryEnum QueuesMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum QueuesMemDataFactory = MemDataFactoryEnum.Data;


        internal const MemFactoryEnum QueueMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum QueueMemDataFactory = MemDataFactoryEnum.Data;


        internal const QueueChildPriorityFactoryEnum QueueChildPriorityFactory = QueueChildPriorityFactoryEnum.Default;
        internal const QueueChildFIFOFactoryEnum QueueChildFIFOFactory = QueueChildFIFOFactoryEnum.Default;
        internal const QueueChildLIFOFactoryEnum QueueChildLIFOFactory = QueueChildLIFOFactoryEnum.Default;

        internal const string DefaultQueueSpecialKey = "queue.default.special";
        internal static ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigDatas = DefaultQueueConfigData();
        private static ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigData()
        {
            var clientDatas = new ConcurrentDictionary<string, DefaultQueueConfigModel>();

            clientDatas.TryAdd("queue.default.special", new DefaultQueueConfigModel
            {
                QueueKey = "queue.default.special",
                MatchType = MatchTypeEnum.Special,
                SendType = SendTypeEnum.FIFO,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            return clientDatas;
        }
    }
}
