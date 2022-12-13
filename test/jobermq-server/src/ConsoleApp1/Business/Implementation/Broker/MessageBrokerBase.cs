using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Business.Abstraction.Broker;
using JoberMQ.Business.Abstraction.Distributor;
using JoberMQ.Business.Abstraction.Queue;
using JoberMQ.Data.InMemory;

namespace JoberMQ.Business.Implementation.Broker
{
    internal abstract class MessageBrokerBase : IMessageBroker
    {
        protected readonly IConcurrentDictionaryRepository<string, IMessageDistributor> MessageDistributors;
        protected readonly IConcurrentDictionaryRepository<string, IMessageQueue> MessageQueues;

        public MessageBrokerBase()
        {
            MessageDistributors= new ConcurrentDictionaryRepository<string, IMessageDistributor>(InMemoryMessageDistributor.Datas);
            MessageQueues = new ConcurrentDictionaryRepository<string, IMessageQueue>(InMemoryMessageQueue.Datas);
        }

        public abstract bool Start();
    }
}
