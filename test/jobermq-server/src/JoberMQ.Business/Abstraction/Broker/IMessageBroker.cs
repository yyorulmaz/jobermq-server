//using JoberMQ.Entities.Dbos;
//using JoberMQ.Entities.Enums.Distributor;
//using JoberMQ.Entities.Enums.Queue;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace JoberMQ.Business.Abstraction.Broker
//{
//    internal interface IMessageBroker
//    {
//        public bool Start();
//        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable);
//        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable);
//        public bool QueueAdd(List<MessageDbo> messages);
//    }
//}
