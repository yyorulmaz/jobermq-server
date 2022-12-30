using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Queue.Abstraction;
using System.Collections.Generic;

namespace JoberMQ.Broker.Abstraction
{
    internal interface IMessageBroker
    {
        IMemRepository<string, IMessageDistributor> MessageDistributors { get; set; }
        IMemRepository<string, IMessageQueue> MessageQueues { get; set; }

        public bool Start();
        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable);
        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable);
        public bool QueueAdd(List<MessageDbo> messages);
    }
}
