using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Config;
using System.Collections.Concurrent;

namespace JoberMQ.Queue.Abstraction
{
    public interface IConfigurationQueue
    {
        public QueueFactoryEnum QueueFactory { get; set; }
        public MemFactoryEnum QueueMemFactory { get; set; }
        public MemDataFactoryEnum QueueMemDataFactory { get; set; }


        public QueueChildPriorityFactoryEnum QueueChildPriorityFactory { get; set; }
        public QueueChildFIFOFactoryEnum QueueChildFIFOFactory { get; set; }
        public QueueChildLIFOFactoryEnum QueueChildLIFOFactory { get; set; }
        public ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigDatas { get; set; }
    }
}
