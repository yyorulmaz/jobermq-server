using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Queue;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    public class DefaultConfigurationQueue : IConfigurationQueue
    {
        QueueFactoryEnum queueFactory = ConfigurationQueueConst.QueueFactory;
        public QueueFactoryEnum QueueFactory { get => queueFactory; set => queueFactory = value; }



        MemFactoryEnum queuesMemFactory = ConfigurationQueueConst.QueuesMemFactory;
        public MemFactoryEnum QueuesMemFactory { get => queuesMemFactory; set => queuesMemFactory = value; }
        MemDataFactoryEnum queuesMemDataFactory = ConfigurationQueueConst.QueuesMemDataFactory;
        public MemDataFactoryEnum QueuesMemDataFactory { get => queuesMemDataFactory; set => queuesMemDataFactory = value; }


        MemFactoryEnum queueMemFactory = ConfigurationQueueConst.QueueMemFactory;
        public MemFactoryEnum QueueMemFactory { get => queueMemFactory; set => queueMemFactory = value; }
        MemDataFactoryEnum queueMemDataFactory = ConfigurationQueueConst.QueueMemDataFactory;
        public MemDataFactoryEnum QueueMemDataFactory { get => queueMemDataFactory; set => queueMemDataFactory = value; }


        public QueueMatchTypeFactoryEnum queueMatchTypeFactory = ConfigurationQueueConst.QueueMatchTypeFactory;
        public QueueMatchTypeFactoryEnum QueueMatchTypeFactory { get => queueMatchTypeFactory; set => queueMatchTypeFactory = value; }


        public QueueOrderOfSendingTypeFactoryEnum queueOrderOfSendingTypeFactory = ConfigurationQueueConst.QueueOrderOfSendingTypeFactory;
        public QueueOrderOfSendingTypeFactoryEnum QueueOrderOfSendingTypeFactory { get => queueOrderOfSendingTypeFactory; set => queueOrderOfSendingTypeFactory = value; }


        public ConcurrentDictionary<string, QueueModel> defaultQueueConfigDatas = ConfigurationQueueConst.DefaultQueueConfigDatas;
        public ConcurrentDictionary<string, QueueModel> DefaultQueueConfigDatas { get => defaultQueueConfigDatas; set => defaultQueueConfigDatas = value; }
    }
}
