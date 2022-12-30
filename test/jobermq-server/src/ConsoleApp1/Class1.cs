//using GenRep.ConcurrentRepository.ConcurrentDictionary;
//using JoberMQ.DataAccess.InMemory;
//using JoberMQ.Entities.Enums.Distributor;
//using JoberMQ.Entities.Enums.Queue;
//using System.Collections.Concurrent;

//namespace JoberMQ.Server.denemeler
//{
//    internal class FactoryHelper
//    {
//        internal static ConcurrentDictionaryRepository<TKey, TValue> Create<TKey, TValue>(ConcurrentDictionary<TKey, TValue> data)
//            => new ConcurrentDictionaryRepository<TKey, TValue>(data);
//    }
//    internal class MessageBrokerFactory
//    {
//        internal static IMessageBroker Create(MessageBrokerFactoryEnum factory)
//        {
//            IMessageBroker messageBroker;

//            switch (factory)
//            {
//                case MessageBrokerFactoryEnum.Default:
//                    messageBroker = new DfMessageBroker();
//                    break;
//                default:
//                    messageBroker = new DfMessageBroker();
//                    break;
//            }

//            return messageBroker;
//        }
//    }
//    internal class MessageDistributorFactory
//    {
//        internal static IMessageDistributor Create(MessageDistributorFactoryEnum factory)
//        {
//            IMessageDistributor messageDistributor;

//            switch (factory)
//            {
//                case MessageDistributorFactoryEnum.Default:
//                    messageDistributor = new DfMessageDistributor();
//                    break;
//                default:
//                    messageDistributor = new DfMessageDistributor();
//                    break;
//            }

//            return messageDistributor;
//        }
//    }
//    internal class MessageQueueFactory
//    {
//        internal static IMessageQueue Create(MessageQueueFactoryEnum factory)
//        {
//            IMessageQueue messageQueue;

//            switch (factory)
//            {
//                case MessageQueueFactoryEnum.Default:
//                    messageQueue = new DfMessageQueue();
//                    break;
//                default:
//                    messageQueue = new DfMessageQueue();
//                    break;
//            }

//            return messageQueue;
//        }
//    }


//    internal interface IMessageBroker
//    {
//        IConcurrentDictionaryRepository<string, IMessageDistributor> MessageDistributors { get; set; }
//        IConcurrentDictionaryRepository<string, IMessageQueue> MessageQueues { get; set; }

//        bool Start(MessageDistributorFactoryEnum messageDistributorFactory, MessageQueueFactoryEnum messageQueueFactory);
//    }
//    internal interface IMessageDistributor
//    {
//    }
//    internal interface IMessageQueue
//    {
//    }


//    internal class DfMessageBroker : IMessageBroker
//    {
//        IConcurrentDictionaryRepository<string, IMessageDistributor> messageDistributors;
//        IConcurrentDictionaryRepository<string, IMessageQueue> messageQueues;

//        public IConcurrentDictionaryRepository<string, IMessageDistributor> MessageDistributors { get { return messageDistributors; } set { messageDistributors = value; } }
//        public IConcurrentDictionaryRepository<string, IMessageQueue> MessageQueues { get { return messageQueues; } set { messageQueues = value; } }

//        public bool Start(MessageDistributorFactoryEnum messageDistributorFactory, MessageQueueFactoryEnum messageQueueFactory)
//        {
//            messageDistributors = FactoryHelper.Create<string, IMessageDistributor>(DboInMemory.DistributorDatas);
//            messageQueues = new ConcurrentDictionaryRepository<string, IMessageQueue>();



//            return true;
//        }
//    }
//    internal class DfMessageDistributor : IMessageDistributor
//    {

//    }
//    internal class DfMessageQueue : IMessageQueue
//    {

//    }
//}
