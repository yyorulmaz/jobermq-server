using JoberMQ.Business.Abstraction.Broker;
using JoberMQ.Data.Abstraction.Repository.DbMem;
using JoberMQ.Data.Implementation.Repository.DbMem.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Business.Implementation.Broker
{
    internal abstract class MessageBrokerBase : IMessageBroker
    {
        protected readonly IDbMemRepository<string, IMessageDistributor> MessageDistributors;
        protected readonly IDbMemRepository<string, IMessageQueue> MessageQueues;

        public MessageBrokerBase()
        {
            MessageDistributors= new DfDbMemRepository<string, IMessageDistributor>(InMemoryMessageDistributor.Datas);
            MessageQueues = new DfDbMemRepository<string, IMessageQueue>(InMemoryMessageQueue.Datas);
        }

        public abstract bool Start();
    }
}
