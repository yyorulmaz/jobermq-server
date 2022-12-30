using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Config;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Constants;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Queue.Implementation.Default
{
    internal class DfConfigurationQueue : IConfigurationQueue
    {
        QueueFactoryEnum queueFactory = DefaultQueueConst.QueueFactory;
        public QueueFactoryEnum QueueFactory { get => queueFactory; set => queueFactory = value; }

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
