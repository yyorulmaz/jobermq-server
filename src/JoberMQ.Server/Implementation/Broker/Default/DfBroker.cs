using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Factories.Distributor;
using JoberMQ.Server.Factories.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using System.Collections.Concurrent;
using System;
using JoberMQNEW.Server.Data;
using JoberMQ.Server.Abstraction.DbOpr;

namespace JoberMQ.Server.Implementation.Broker.Default
{
    internal class DfBroker : IBroker
    {
        IClientService clientService;
        IDbOprService dbOprService;
        IConcurrentDictionaryRepository<string, IDistributor> distributors;
        IConcurrentDictionaryRepository<string, IQueue> queues;
        ServerConfigModel serverConfig;
        public DfBroker(
            ServerConfigModel serverConfig,
            IDbOprService dbOprService,
            IClientService clientService)
        {
            this.serverConfig = serverConfig;
            this.dbOprService = dbOprService;
            this.clientService = clientService;
        }



        public bool Start()
        {
            distributors = new ConcurrentDictionaryRepository<string, IDistributor>(InMemoryDistributor.DistributorDatas);
            queues = new ConcurrentDictionaryRepository<string, IQueue>(InMemoryQueue.QueuesDatas);


            var dbDistributors = dbOprService.Distributor.GetAll(x => x.IsActive == true);
            foreach (var item in dbDistributors)
            {
                var dis = DistributorFactory.CreateDistributor(serverConfig.BrokerConfig.DistributorFactory, item.DistributorKey, item.DistributorType);
                distributors.Add(item.DistributorKey, dis);
            }


            var dbQueues = dbOprService.Queue.GetAll(x => x.IsActive == true);
            foreach (var item in dbQueues)
            {
                var clientGroup = clientService.AddClientGroup(item.QueueKey);
                var que = QueueFactory.CreateQueue(serverConfig.BrokerConfig, item.QueueKey, item.MatchType, item.SendType, clientGroup, dbOprService.Message);
                queues.Add(item.QueueKey, que);
            }





            // default ddistributorlar ayağa kalkmamışsa oluştur


            // default queue lar ayağa kalkmamışsa oluştur


            // kuyruk mesajlarını kuyruklara aktar


            // client gruplarını nasıl olacak. Bir client login olduğunda o işlemler kontrol edilir


            return true;
        }


        public bool DistributorCreate(string name, DistributorTypeEnum type)
        {
            // todo kuşullar sağlandımı kontrol
            var distributor = DistributorFactory.CreateDistributor(serverConfig.BrokerConfig.DistributorFactory, name, type);
            distributors.Add(name, distributor);
            return true;
        }

        public bool QueueCreate(string distributorName, string queueName, MatchTypeEnum matchType, SendTypeEnum sendType)
        {
            // todo kuşullar sağlandımı kontrol
            var clientGroup = clientService.AddClientGroup(queueName);

            var queue = QueueFactory.CreateQueue(
                serverConfig.BrokerConfig,
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
