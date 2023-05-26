using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Configuration;
using JoberMQ.Common.Models.Queue;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultQueueConst
    {
        internal const QueueFactoryEnum QueueFactory = QueueFactoryEnum.Default;


        internal const MemFactoryEnum QueuesMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum QueuesMemDataFactory = MemDataFactoryEnum.None;


        internal const MemFactoryEnum QueueMemFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum QueueMemDataFactory = MemDataFactoryEnum.Data;


        internal const QueueChildPriorityFactoryEnum QueueChildPriorityFactory = QueueChildPriorityFactoryEnum.Default;
        internal const QueueChildFIFOFactoryEnum QueueChildFIFOFactory = QueueChildFIFOFactoryEnum.Default;
        internal const QueueChildLIFOFactoryEnum QueueChildLIFOFactory = QueueChildLIFOFactoryEnum.Default;

        //internal const string DefaultQueueSpecialKey = "queue.default.special";
        internal static ConcurrentDictionary<string, QueueModel> DefaultQueueConfigDatas = DefaultQueueConfigData();
        private static ConcurrentDictionary<string, QueueModel> DefaultQueueConfigData()
        {
            var clientDatas = new ConcurrentDictionary<string, QueueModel>();

            clientDatas.TryAdd("default.queue.special.fifo", new QueueModel
            {
                QueueKey = "default.queue.special.fifo",
                QueueMatchType = QueueMatchTypeEnum.Special,
                QueueOrderOfSendingType = QueueOrderOfSendingTypeEnum.FIFO,
                PermissionType = PermissionTypeEnum.All,
                IsDurable = true
            });

            


            return clientDatas;
        }


        internal const bool IsGroupQueueCreate = true;
        internal static QueueModel NewClientGroupData = new QueueModel {
            QueueKey = null,
            QueueMatchType = QueueMatchTypeEnum.Group,
            QueueOrderOfSendingType = QueueOrderOfSendingTypeEnum.FIFO,
            PermissionType = PermissionTypeEnum.All,
            IsDurable = true
        };

    }
}
