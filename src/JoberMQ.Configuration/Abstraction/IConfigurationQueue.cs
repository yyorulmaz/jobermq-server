using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Configuration;
using JoberMQ.Common.Models.Queue;
using System.Collections.Concurrent;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationQueue
    {
        public QueueFactoryEnum QueueFactory { get; set; }

        public MemFactoryEnum QueuesMemFactory { get; set; }
        public MemDataFactoryEnum QueuesMemDataFactory { get; set; }

        public MemFactoryEnum QueueMemFactory { get; set; }
        public MemDataFactoryEnum QueueMemDataFactory { get; set; }


        public QueueChildPriorityFactoryEnum QueueChildPriorityFactory { get; set; }
        public QueueChildFIFOFactoryEnum QueueChildFIFOFactory { get; set; }
        public QueueChildLIFOFactoryEnum QueueChildLIFOFactory { get; set; }
        public ConcurrentDictionary<string, QueueModel> DefaultQueueConfigDatas { get; set; }

        public bool IsGroupQueueCreate { get; set; }
    }
}
