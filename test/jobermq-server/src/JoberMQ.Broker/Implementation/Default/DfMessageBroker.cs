using JoberMQ.Broker.Abstraction;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using System;
using System.Collections.Generic;

namespace JoberMQ.Broker.Implementation.Default
{
    internal class DfMessageBroker : IMessageBroker
    {
        public bool Start()
        {
            throw new NotImplementedException();
        }

        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable)
        {
            throw new NotImplementedException();
        }
        public bool DistributorRemove(string distributorKey)
        {
            throw new NotImplementedException();
        }

        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable)
        {
            throw new NotImplementedException();
        }
        public bool QueueRemove(string queueKey)
        {
            throw new NotImplementedException();
        }
        public bool QueueRemove(string distributorName, string queueKey)
        {
            throw new NotImplementedException();
        }
        public bool QueueAdd(List<MessageDbo> messages)
        {
            throw new NotImplementedException();
        }
    }
}
