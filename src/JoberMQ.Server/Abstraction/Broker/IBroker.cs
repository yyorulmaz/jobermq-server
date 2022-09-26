using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.Broker
{
    internal interface IBroker
    {

        public bool Start();
        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable);
        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable);
        public bool QueueAdd(List<MessageDbo> messages);
    }
}
