using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Factories;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Data;
using JoberMQ.Queue.Factories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace JoberMQ.Server.Implementation.Broker.Default
{
    internal class DfMessageBroker<THub> : IMessageBroker
        where THub : Hub
    {
        IMemRepository<Guid, MessageDbo> messageMaster;
        IMemRepository<string, IClient> clientMaster;
        IMemRepository<string, IMessageDistributor> messageDistributors;
        public IMemRepository<string, IMessageDistributor> MessageDistributors { get => messageDistributors; set => messageDistributors = value; }
        IMemRepository<string, IMessageQueue> messageQueues;
        public IMemRepository<string, IMessageQueue> MessageQueues { get => messageQueues; set => messageQueues = value; }



        IConfiguration configuration;
        IDatabase database;
        IHubContext<THub> hubContext;
        bool isJoberActive;

        public DfMessageBroker(
            IConfiguration configuration,
            IMemRepository<Guid, MessageDbo> messageMaster,
            IMemRepository<string, IClient> clientMaster,
            IDatabase database,
            IHubContext<THub> hubContext,
            ref bool isJoberActive)
        {
            this.configuration = configuration;
            this.database = database;
            this.hubContext = hubContext;
            this.isJoberActive = isJoberActive;

            this.messageMaster = messageMaster;
            this.clientMaster = clientMaster;
            messageDistributors = MemFactory.Create<string, IMessageDistributor>(configuration.ConfigurationDistributor.DistributorsMemFactory, configuration.ConfigurationDistributor.DistributorsMemDataFactory);
            messageQueues = MemFactory.Create<string, IMessageQueue>(configuration.ConfigurationQueue.QueuesMemFactory, configuration.ConfigurationQueue.QueuesMemDataFactory);


            ImportDatabaseDistributor();
            ImportDatabaseQueue();

            CreateDefaultDistributor();
            CreateDefaultQueue();
        }
        void ImportDatabaseDistributor()
        {
            var dbDistributors = database.Distributor.GetAll(x => x.IsActive == true);
            foreach (var item in dbDistributors)
            {
                var dis = DistributorFactory.CreateDistributor(configuration.ConfigurationDistributor.DistributorFactory, item.DistributorKey, item.DistributorType, item.PermissionType, item.IsDurable, messageQueues);
                messageDistributors.Add(item.DistributorKey, dis);
            }
        }
        void ImportDatabaseQueue()
        {
            var dbQueues = database.Queue.GetAll(x => x.IsActive == true);
            foreach (var item in dbQueues)
            {
                var clientChild = MemChildFactory.CreateChildGeneral<string, IClient>(Library.Database.Enums.MemChildFactoryEnum.Default, clientMaster, false, true, true);
                var que = QueueFactory.Create<THub>(configuration.ConfigurationQueue, item.QueueKey, item.MatchType, item.SendType, item.PermissionType, item.IsDurable, clientMaster, messageMaster, database.Message, ref isJoberActive, hubContext);
                messageQueues.Add(item.QueueKey, que);
            }
        }
        void CreateDefaultDistributor()
        {
            foreach (var item in configuration.ConfigurationDistributor.DefaultDistributorConfigDatas)
            {
                if (messageDistributors != null && messageDistributors.MasterData != null)
                {
                    var checkDistributor = messageDistributors.Get(item.Key);
                    if (checkDistributor == null)
                    {
                        var dis = DistributorFactory.CreateDistributor(configuration.ConfigurationDistributor.DistributorFactory, item.Value.DistributorKey, item.Value.DistributorType, item.Value.PermissionType, item.Value.IsDurable, messageQueues);
                        messageDistributors.Add(item.Value.DistributorKey, dis);
                    }
                }
            }
        }
        void CreateDefaultQueue()
        {
            foreach (var item in configuration.ConfigurationQueue.DefaultQueueConfigDatas)
            {
                if (messageQueues != null && messageQueues.MasterData != null)
                {
                    var checkQueue = messageQueues.Get(item.Key);
                    if (checkQueue == null)
                    {
                        var que = QueueFactory.Create<THub>(configuration.ConfigurationQueue, item.Value.QueueKey, item.Value.MatchType, item.Value.SendType, item.Value.PermissionType, item.Value.IsDurable, clientMaster, messageMaster, database.Message, ref isJoberActive, hubContext);
                        messageQueues.Add(item.Value.QueueKey, que);
                    }
                }
            }
        }





        public bool CreateDistributor(string distributorKey, DistributorTypeEnum distributorType, bool isDurable)
        {
            // todo kuşullar sağlandımı kontrol
            var distributor = DistributorFactory.CreateDistributor(configuration.ConfigurationDistributor.DistributorFactory, distributorKey, distributorType, PermissionTypeEnum.All, isDurable, messageQueues);
            messageDistributors.Add(distributorKey, distributor);
            return true;
        }
        public bool CreateQueue(string distributorName, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, bool isDurable)
        {
            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)

            var queue = QueueFactory.Create<THub>(
                configuration.ConfigurationQueue,
                queueKey,
                matchType,
                sendType,
                PermissionTypeEnum.All,
                isDurable,
                clientMaster,
                messageMaster,
                database.Message,
                ref isJoberActive,
                hubContext);

            return true;
        }



        public bool Start()
        {

            // todo filitreyi düzenle burası yanlış olacak büyük ihtimalle
            var messages = database.Message.GetAll(x => x.IsActive == true && x.IsDelete ==false && x.StatusTypeMessage == StatusTypeMessageEnum.None);
            QueueAdd(messages);

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
                var addResult = distributor.MessageAdd(msg);

                if (!addResult)
                {
                    isError = true;
                    continue;
                }
            }

            if (isError)
            {
                foreach (var msg in messages)
                    database.Message.Rollback(msg.Id, msg);

                return false;
            }

            return true;
        }
    }
}
