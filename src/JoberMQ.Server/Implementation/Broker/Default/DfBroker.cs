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
using JoberMQ.Entities.Enums.Permission;

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
                var dis = DistributorFactory.CreateDistributor(serverConfig.BrokerConfig.DistributorFactory, item.DistributorKey, item.DistributorType, item.PermissionType, item.IsDurable);
                distributors.Add(item.DistributorKey, dis);
            }


            var dbQueues = dbOprService.Queue.GetAll(x => x.IsActive == true);
            foreach (var item in dbQueues)
            {
                var clientGroup = clientService.AddClientGroup(item.QueueKey);
                var que = QueueFactory.CreateQueue(serverConfig.BrokerConfig, item.QueueKey, item.MatchType, item.SendType, item.PermissionType, item.IsDurable, clientGroup, dbOprService.Message);
                queues.Add(item.QueueKey, que);
            }


            foreach (var item in serverConfig.BrokerConfig.DefaultDistributorConfigDatas)
            {
                var checkDistributor = distributors.Get(item.Key);
                if (checkDistributor == null)
                {
                    var dis = DistributorFactory.CreateDistributor(serverConfig.BrokerConfig.DistributorFactory, item.Value.DistributorKey, item.Value.DistributorType, item.Value.PermissionType, item.Value.IsDurable);
                    distributors.Add(item.Value.DistributorKey, dis);
                }
            }


            foreach (var item in serverConfig.BrokerConfig.DefaultQueueConfigDatas)
            {
                var checkQueue = queues.Get(item.Key);
                if (checkQueue == null)
                {
                    var clientGroup = clientService.AddClientGroup(item.Value.QueueKey);
                    var que = QueueFactory.CreateQueue(serverConfig.BrokerConfig, item.Value.QueueKey, item.Value.MatchType, item.Value.SendType, item.Value.PermissionType, item.Value.IsDurable, clientGroup, dbOprService.Message);
                    queues.Add(item.Value.QueueKey, que);
                }
            }


            // todo filitreyi düzenle burası yanlış olacak büyük ihtimalle
            var messages = dbOprService.Message.GetAll(x=>x.IsActive == true && x.IsDelete ==false && x.StatusTypeMessage == Entities.Enums.Status.StatusTypeMessageEnum.None);
            foreach (var item in messages)
            {
                QueueAdd(item);
            }

            return true;
        }


        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable)
        {
            // todo kuşullar sağlandımı kontrol
            var distributor = DistributorFactory.CreateDistributor(serverConfig.BrokerConfig.DistributorFactory, distributorKey, distributorType, PermissionTypeEnum.All, isDurable);
            distributors.Add(distributorKey, distributor);
            return true;
        }

        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable)
        {
            // todo kuşullar sağlandımı kontrol
            var clientGroup = clientService.AddClientGroup(queueKey);

            var queue = QueueFactory.CreateQueue(
                serverConfig.BrokerConfig,
                queueKey,
                matchType,
                sendType,
                PermissionTypeEnum.All,
                isDurable,
                clientGroup,
                dbOprService.Message);

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
