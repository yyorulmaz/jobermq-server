using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Data;
using JoberMQ.Distributor.Factories;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Data;
using JoberMQ.Queue.Factories;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace JoberMQ.Server.Implementation.Broker.Default
{
    internal class DfMessageBroker<THubContext> : IMessageBroker where THubContext : Hub
    {
        IConfigurationDistributor configurationDistributor;
        IConfigurationQueue configurationQueue;
        IDatabaseService databaseService;
        IClientService clientService;
        IHubContext<THubContext> context;

        public DfMessageBroker(
            IConfigurationDistributor configurationDistributor,
            IConfigurationQueue configurationQueue,
            IDatabaseService databaseService,
            IClientService clientService,
            IHubContext<THubContext> context)
        {
            this.configurationDistributor = configurationDistributor;
            this.configurationQueue = configurationQueue;
            this.databaseService = databaseService;
            this.clientService = clientService;
            this.context = context;

            messageDistributors = MemFactory.CreateMem<string, IMessageDistributor>(configurationDistributor.DistributorMemFactory, configurationDistributor.DistributorMemDataFactory, InMemoryDistributor.DistributorDatas);
            messageQueues = MemFactory.CreateMem<string, IMessageQueue>(configurationQueue.QueueMemFactory, configurationQueue.QueueMemDataFactory, InMemoryQueue.QueuesDatas);
        }


        IMemRepository<string, IMessageDistributor> messageDistributors;
        IMemRepository<string, IMessageQueue> messageQueues;
        public IMemRepository<string, IMessageDistributor> MessageDistributors { get => messageDistributors; set => messageDistributors = value; }
        public IMemRepository<string, IMessageQueue> MessageQueues { get => messageQueues; set => messageQueues = value; }

        public bool Start()
        {
            var dbDistributors = databaseService.Distributor.GetAll(x => x.IsActive == true);
            foreach (var item in dbDistributors)
            {
                var dis = DistributorFactory.CreateDistributor(configurationDistributor.DistributorFactory, item.DistributorKey, item.DistributorType, item.PermissionType, item.IsDurable, messageQueues);
                messageDistributors.Add(item.DistributorKey, dis);
            }


            var dbQueues = databaseService.Queue.GetAll(x => x.IsActive == true);
            foreach (var item in dbQueues)
            {
                var clientGroup = clientService.AddClientGroup(item.QueueKey);
                var que = QueueFactory.CreateQueue<THubContext>(configurationQueue, item.QueueKey, item.MatchType, item.SendType, item.PermissionType, item.IsDurable, clientGroup, databaseService.Message, context);
                messageQueues.Add(item.QueueKey, que);
            }


            foreach (var item in configurationDistributor.DefaultDistributorConfigDatas)
            {
                var checkDistributor = messageDistributors.Get(item.Key);
                if (checkDistributor == null)
                {
                    var dis = DistributorFactory.CreateDistributor(configurationDistributor.DistributorFactory, item.Value.DistributorKey, item.Value.DistributorType, item.Value.PermissionType, item.Value.IsDurable, messageQueues);
                    messageDistributors.Add(item.Value.DistributorKey, dis);
                }
            }


            foreach (var item in configurationQueue.DefaultQueueConfigDatas)
            {
                var checkQueue = messageQueues.Get(item.Key);
                if (checkQueue == null)
                {
                    var clientGroup = clientService.AddClientGroup(item.Value.QueueKey);
                    var que = QueueFactory.CreateQueue<THubContext>(configurationQueue, item.Value.QueueKey, item.Value.MatchType, item.Value.SendType, item.Value.PermissionType, item.Value.IsDurable, clientGroup, databaseService.Message,context);
                    messageQueues.Add(item.Value.QueueKey, que);
                }
            }


            // todo filitreyi düzenle burası yanlış olacak büyük ihtimalle
            var messages = databaseService.Message.GetAll(x=>x.IsActive == true && x.IsDelete ==false && x.StatusTypeMessage == StatusTypeMessageEnum.None);
            QueueAdd(messages);

            return true;
        }
        public bool DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, bool isDurable)
        {
            // todo kuşullar sağlandımı kontrol
            var distributor = DistributorFactory.CreateDistributor(configurationDistributor.DistributorFactory, distributorKey, distributorType, PermissionTypeEnum.All, isDurable, messageQueues);
            messageDistributors.Add(distributorKey, distributor);
            return true;
        }
        public bool QueueCreate(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable)
        {
            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)
            var clientGroup = clientService.AddClientGroup(queueKey);

            var queue = QueueFactory.CreateQueue<THubContext>(
                configurationQueue,
                queueKey,
                matchType,
                sendType,
                PermissionTypeEnum.All,
                isDurable,
                clientGroup,
                databaseService.Message,
                context);

            return true;
        }
        public bool QueueAdd(List<MessageDbo> messages)
        {
            // todo kuşullar sağlandımı kontrol


            // TODO BURADA BÖYLE BİR YAPI KURMAYA GEREK YOK YAVAŞLATIR
            // CLIENT TARAFINDAN DistributorKey VE RoutingKey DOLU OLARAK GELECEK
            // OZAMAN RoutingType 'a GEREK KALIR MI


            bool isError = false;
            foreach (var msg in messages)
            {
                var distributor = messageDistributors.Get(msg.DistributorKey);
                var addResult = distributor.QueueAdd(msg);

                if (!addResult)
                {
                    isError = true;
                    continue;
                }
            }
            
            if (isError)
            {
                foreach (var msg in messages)
                    databaseService.Message.Rollback(msg);

                return false;
            }

            return true;
        }
    }
}
