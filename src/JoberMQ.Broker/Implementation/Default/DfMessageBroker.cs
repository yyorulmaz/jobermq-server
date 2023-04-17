using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Models.Base;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Factories;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Factories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        IStatusCode statusCode;
        IDatabase database;
        IHubContext<THub> hubContext;
        bool isJoberActive;

        public DfMessageBroker(
            IConfiguration configuration,
            IStatusCode statusCode,
            IMemRepository<Guid, MessageDbo> messageMaster,
            IMemRepository<string, IClient> clientMaster,
            IDatabase database,
            IHubContext<THub> hubContext,
            ref bool isJoberActive)
        {
            this.configuration = configuration;
            this.statusCode = statusCode;
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

            ImportMessage();
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
                var que = QueueFactory.Create<THub>(configuration.ConfigurationQueue, item.DistributorKey, item.QueueKey, item.MatchType, item.SendType, item.PermissionType, item.IsDurable, clientMaster, messageMaster, database.Message, ref isJoberActive, hubContext);
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
                else
                {
                    var dis = DistributorFactory.CreateDistributor(configuration.ConfigurationDistributor.DistributorFactory, item.Value.DistributorKey, item.Value.DistributorType, item.Value.PermissionType, item.Value.IsDurable, messageQueues);
                    messageDistributors.Add(item.Value.DistributorKey, dis);
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
                        var que = QueueFactory.Create<THub>(configuration.ConfigurationQueue, item.Value.DistributorKey, item.Value.QueueKey, item.Value.MatchType, item.Value.SendType, item.Value.PermissionType, item.Value.IsDurable, clientMaster, messageMaster, database.Message, ref isJoberActive, hubContext);
                        messageQueues.Add(item.Value.QueueKey, que);
                    }
                }
            }
        }

        void ImportMessage()
        {
            // todo filitreyi düzenle burası yanlış olacak büyük ihtimalle
            var messages = database.Message.GetAll(x => x.IsActive == true && x.IsDelete ==false && x.Status.StatusTypeMessage == StatusTypeMessageEnum.None);
            QueueAdd(messages);
        }



        public async Task<ResponseBaseModel> DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                if (distributor != null)
                {
                    result.IsSuccess = false;
                    result.Message = statusCode.GetStatusMessage("1.7.1");
                }
                else
                {
                    var newDistributor = DistributorFactory.CreateDistributor(configuration.ConfigurationDistributor.DistributorFactory, distributorKey, distributorType, PermissionTypeEnum.All, isDurable, messageQueues);
                    var resultDistributorAdd = messageDistributors.Add(distributorKey, newDistributor);

                    if (resultDistributorAdd == true)
                    {
                        result.IsSuccess = true;
                        result.Message = statusCode.GetStatusMessage("1.7.2");
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = statusCode.GetStatusMessage("1.7.3");
                    }
                }
            });
            
            return result;
        }
        public async Task<ResponseBaseModel> DistributorUpdate(string distributorKey, bool isDurable)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                if (distributor == null)
                {
                    result.IsSuccess = false;
                    result.Message = statusCode.GetStatusMessage("1.7.4");
                }
                else
                {
                    distributor.IsDurable = isDurable;
                    var resultDistributorUpdate = messageDistributors.Update(distributorKey, distributor);

                    if (resultDistributorUpdate == true)
                    {
                        result.IsSuccess = true;
                        result.Message = statusCode.GetStatusMessage("1.7.5");
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = statusCode.GetStatusMessage("1.7.6");
                    }
                }
            });
            
            return result;
        }
        public async Task<ResponseBaseModel> DistributorRemove(string distributorKey)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                if (distributor == null)
                {
                    result.IsSuccess = false;
                    result.Message = statusCode.GetStatusMessage("1.7.4");
                }
                else
                {
                    var resultDistributorRemove = messageDistributors.Remove(distributorKey);
                    if (resultDistributorRemove != null)
                    {
                        result.IsSuccess = true;
                        result.Message = statusCode.GetStatusMessage("1.7.7");
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = statusCode.GetStatusMessage("1.7.8");
                    }
                }
            });



            return result;
        }



        public async Task<ResponseBaseModel> QueueCreate(string distributorKey, string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var queue = messageQueues.Get(queueKey);
                if (queue != null)
                {
                    result.IsSuccess = false;
                    result.Message = statusCode.GetStatusMessage("1.7.9");
                }
                else
                {
                    var newtQueue = QueueFactory.Create<THub>(
                    configuration.ConfigurationQueue,
                    distributorKey,
                    queueKey,
                    matchType,
                    sendType,
                    permissionType,
                    isDurable,
                    clientMaster,
                    messageMaster,
                    database.Message,
                    ref isJoberActive,
                    hubContext);
                    var resultQueueAdd = messageQueues.Add(queueKey, newtQueue);

                    if (resultQueueAdd == true)
                    {
                        result.IsSuccess = true;
                        result.Message = statusCode.GetStatusMessage("1.7.10");
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = statusCode.GetStatusMessage("1.7.11");
                    }
                }

            });

            return result;

            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)


        }
        public async Task<ResponseBaseModel> QueueUpdate(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseBaseModel();
            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)

            await Task.Run(() =>
            {
                var queue = messageQueues.Get(queueKey);
                if (queue != null)
                {
                    result.IsSuccess = false;
                    result.Message = statusCode.GetStatusMessage("1.7.12");
                }
                else
                {
                    queue.MatchType = matchType;
                    queue.SendType = sendType;
                    queue.PermissionType = permissionType;
                    queue.IsDurable = isDurable;


                    var resultQueueAdd = messageQueues.Update(queueKey, queue);

                    if (resultQueueAdd == true)
                    {
                        result.IsSuccess = true;
                        result.Message = statusCode.GetStatusMessage("1.7.13");
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = statusCode.GetStatusMessage("1.7.14");
                    }
                }

            });

            return result;
        }
        public async Task<ResponseBaseModel> QueueRemove(string queueKey)
        {
            var result = new ResponseBaseModel();
            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)

            await Task.Run(() =>
            {
                var queue = messageQueues.Get(queueKey);
                if (queue != null)
                {
                    result.IsSuccess = false;
                    result.Message = statusCode.GetStatusMessage("1.7.15");
                }
                else
                {
                    queue.MatchType = matchType;
                    queue.SendType = sendType;
                    queue.PermissionType = permissionType;
                    queue.IsDurable = isDurable;


                    var resultQueueAdd = messageQueues.Update(queueKey, queue);

                    if (resultQueueAdd == true)
                    {
                        result.IsSuccess = true;
                        result.Message = statusCode.GetStatusMessage("1.7.13");
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = statusCode.GetStatusMessage("1.7.14");
                    }
                }
            });


            return result;
        }
        public async Task<ResponseBaseModel> QueueBind(string distributorKey, string queueKey)
        {
            var result = new ResponseBaseModel();
            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)

            await Task.Run(() =>
            {

            });


            return result;
        }

        public bool MessageAdd(MessageDbo message)
            => messageDistributors.Get(message.Message.Routing.DistributorKey).MessageAdd(message);

        public bool QueueAdd(List<MessageDbo> messages)
        {
            // todo kuşullar sağlandımı kontrol


            // TODO BURADA BÖYLE BİR YAPI KURMAYA GEREK YOK YAVAŞLATIR
            // CLIENT TARAFINDAN DistributorKey VE RoutingKey DOLU OLARAK GELECEK
            // OZAMAN RoutingType 'a GEREK KALIR MI


            bool isError = false;
            foreach (var msg in messages)
            {
                var distributor = messageDistributors.Get(msg.Message.Routing.DistributorKey);
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
