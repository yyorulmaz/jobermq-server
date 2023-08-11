using System.Collections.Concurrent;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Queue;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationQueue
    {
        public QueueFactoryEnum QueueFactory { get; set; }

        public MemFactoryEnum QueuesMemFactory { get; set; }
        public MemDataFactoryEnum QueuesMemDataFactory { get; set; }

        public MemFactoryEnum QueueMemFactory { get; set; }
        public MemDataFactoryEnum QueueMemDataFactory { get; set; }


        public QueueMatchTypeFactoryEnum QueueMatchTypeFactory { get; set; }
        public QueueOrderOfSendingTypeFactoryEnum QueueOrderOfSendingTypeFactory { get; set; }

        public ConcurrentDictionary<string, QueueModel> DefaultQueueConfigDatas { get; set; }
    }
}
