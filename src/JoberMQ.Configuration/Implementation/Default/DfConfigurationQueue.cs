using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Config;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Database.Enums;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationQueue : IConfigurationQueue
    {
















        QueueFactoryEnum queueFactory = DefaultQueueConst.QueueFactory;
        public QueueFactoryEnum QueueFactory { get => queueFactory; set => queueFactory = value; }



        MemFactoryEnum queuesMemFactory = DefaultQueueConst.QueuesMemFactory;
        public MemFactoryEnum QueuesMemFactory { get => queuesMemFactory; set => queuesMemFactory = value; }
        MemDataFactoryEnum queuesMemDataFactory = DefaultQueueConst.QueuesMemDataFactory;
        public MemDataFactoryEnum QueuesMemDataFactory { get => queuesMemDataFactory; set => queuesMemDataFactory = value; }


        MemFactoryEnum queueMemFactory = DefaultQueueConst.QueueMemFactory;
        public MemFactoryEnum QueueMemFactory { get => queueMemFactory; set => queueMemFactory = value; }
        MemDataFactoryEnum queueMemDataFactory = DefaultQueueConst.QueueMemDataFactory;
        public MemDataFactoryEnum QueueMemDataFactory { get => queueMemDataFactory; set => queueMemDataFactory = value; }


        public QueueChildPriorityFactoryEnum queueChildPriorityFactory = DefaultQueueConst.QueueChildPriorityFactory;
        public QueueChildPriorityFactoryEnum QueueChildPriorityFactory { get => queueChildPriorityFactory; set => queueChildPriorityFactory = value; }


        public QueueChildFIFOFactoryEnum queueChildFIFOFactory = DefaultQueueConst.QueueChildFIFOFactory;
        public QueueChildFIFOFactoryEnum QueueChildFIFOFactory { get => queueChildFIFOFactory; set => queueChildFIFOFactory = value; }


        public QueueChildLIFOFactoryEnum queueChildLIFOFactory = DefaultQueueConst.QueueChildLIFOFactory;
        public QueueChildLIFOFactoryEnum QueueChildLIFOFactory { get => queueChildLIFOFactory; set => queueChildLIFOFactory = value; }


        public ConcurrentDictionary<string, DefaultQueueConfigModel> defaultQueueConfigDatas = DefaultQueueConst.DefaultQueueConfigDatas;
        public ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigDatas { get => defaultQueueConfigDatas; set => defaultQueueConfigDatas = value; }
    }
}
