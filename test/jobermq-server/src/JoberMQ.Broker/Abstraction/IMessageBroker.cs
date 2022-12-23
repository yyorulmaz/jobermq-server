using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using System.Collections.Generic;

namespace JoberMQ.Broker.Abstraction
{
    internal interface IMessageBroker
    {
        public bool Start();
        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable);
        public bool DistributorRemove(string distributorKey);
        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable);
        public bool QueueRemove(string queueKey);
        public bool QueueRemove(string distributorName, string queueKey);
        public bool QueueAdd(List<MessageDbo> messages);
    }
}
