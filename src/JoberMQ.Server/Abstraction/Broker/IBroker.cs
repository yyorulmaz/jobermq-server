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

    internal class DfBroker : IBroker
    {
        public IClientService clientService;
        public IConcurrentDictionaryRepository<string, IDistributor> distributors;
        public IConcurrentDictionaryRepository<string, IQueue> queues;
        ServerConfigModel serverConfig;
        public DfBroker(
            ServerConfigModel serverConfig,
            IClientService clientService,
            IConcurrentDictionaryRepository<string, IDistributor> distributors,
            IConcurrentDictionaryRepository<string, IQueue> queues)
        {
            this.serverConfig = serverConfig;
            this.clientService = clientService;
            this.distributors = distributors;
            this.queues = queues;
        }

        public IClientService ClientService => clientService;
        public IConcurrentDictionaryRepository<string, IDistributor> Distributors => distributors;
        public IConcurrentDictionaryRepository<string, IQueue> Queues => queues;

        public bool DistributorCreate(string name, DistributorTypeEnum type)
        {
            // todo kuşullar sağlandımı kontrol
            var distributor = DistributorFactory.CreateDistributor(serverConfig.BrokerConfig.DistributorFactory, name, type);
            distributors.Add(name, distributor);
            return true;
        }
        
        public bool QueueCreate(
            string distributorName,
            string queueName,
            MatchTypeEnum matchType,
            SendTypeEnum sendType)
        {
            // todo kuşullar sağlandımı kontrol
            var clientGroup = clientService.AddClientGroup(queueName);

            var queue = QueueFactory.CreateQueue(
                serverConfig.BrokerConfig,
                serverConfig.BrokerConfig.QueueFactory,
                distributorName,
                queueName,
                matchType,
                sendType,
                clientGroup,
                Factory.Server.DbOprService.Message);

            return true;
        }

        public JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            //// todo kuşullar sağlandımı kontrol
            //var distributorName = queues.Get(message.QueueKey).DistributorName;
            //var distributor = distributors.Get(distributorName);
            //return distributor.QueueAdd(message);


            return null;
        }
    }
}
