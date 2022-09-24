using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server
{
    public static class QueueType
    {
        internal const string Special = "Special";
        internal const string Group = "Group";
        internal const string Free = "Free";
    }



    internal interface IBroker
    {
        public IClientService ClientService { get; }
        public IConcurrentDictionaryRepository<string, IDistributor> Distributors { get; }
        public IConcurrentDictionaryRepository<string, IQueue> Queues { get; }

        public bool DistributorCreate(string name, DistributorTypeEnum type);
        public bool QueueCreate(
            string distributorName,
            string queueName,
            MatchTypeEnum matchType,
            SendTypeEnum sendType);
        public JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}
