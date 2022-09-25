using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Factories.Distributor;
using JoberMQ.Server.Factories.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Abstraction.Broker
{
    internal interface IBroker
    {

        public bool Start();
        public bool DistributorCreate(string name, DistributorTypeEnum type);
        public bool QueueCreate(
            string distributorName,
            string queueName,
            MatchTypeEnum matchType,
            SendTypeEnum sendType);
        public JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}
