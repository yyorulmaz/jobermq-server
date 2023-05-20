using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Database.Abstraction;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Factories;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Enums.Routing;
using JoberMQ.Library.Enums.Status;
using JoberMQ.Library.Models.Base;
using JoberMQ.Library.Models.Queue;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Factories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoberMQ.Broker.Implementation.Default
{
    //todo distributor ve queue işlemlerinde distributor e bağlı queue bağlı ise işlem yapılmayabilir veya queue içerisinde mesajlar var ise işlem yaptırmayabiliriz butarz durumları düşün
    internal class DfMessageBroker<THub> : IMessageBroker
     where THub : Hub
    {
        IMemRepository<Guid, MessageDbo> messageMaster;
        IClientMasterData clientMasterData;
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
            IClientMasterData clientMasterData,
            IDatabase database,
            IHubContext<THub> hubContext)
        {
            this.configuration = configuration;
            this.statusCode = statusCode;
            this.database = database;
            this.hubContext = hubContext;

            this.messageMaster = messageMaster;
            this.clientMasterData = clientMasterData;
            messageDistributors = MemFactory.Create<string, IMessageDistributor>(configuration.ConfigurationDistributor.DistributorsMemFactory, configuration.ConfigurationDistributor.DistributorsMemDataFactory);
            messageQueues = MemFactory.Create<string, IMessageQueue>(configuration.ConfigurationQueue.QueuesMemFactory, configuration.ConfigurationQueue.QueuesMemDataFactory);


            //ImportDatabaseDistributors();
            //ImportDatabaseQueues();

            //CreateDefaultDistributors();
            //CreateDefaultQueues();

            //ImportMessage();
        }
        public event Action<bool> IsJoberActiveEvent;


        public async Task<ResponseBaseModel> ImportDatabaseDistributors()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = statusCode.GetStatusMessage("1.7.1");

            foreach (var item in database.Distributor.GetAll(x => x.IsActive == true))
            {
                try
                {
                    var dis = DistributorFactory.Create(configuration,  statusCode, configuration.ConfigurationDistributor.DistributorFactory, item.DistributorKey, item.DistributorType, item.PermissionType, item.IsDurable, messageQueues, database);
                    messageDistributors.Add(item.DistributorKey, dis);
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.2");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> ImportDatabaseQueues()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = statusCode.GetStatusMessage("1.7.3");

            foreach (var item in database.Queue.GetAll(x => x.IsActive == true))
            {
                try
                {
                    var que = QueueFactory.Create<THub>(configuration, database, item.QueueKey, item.MatchType, item.SendType, item.PermissionType, item.IsDurable, clientMasterData, messageMaster, database.Message, ref hubContext);
                    messageQueues.Add(item.QueueKey, que);
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.4");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> CreateDefaultDistributors()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = statusCode.GetStatusMessage("1.7.5");

            foreach (var item in configuration.ConfigurationDistributor.DefaultDistributorConfigDatas)
            {
                try
                {
                    if (messageDistributors.Get(item.Key) == null)
                    {
                        var dis = DistributorFactory.Create(configuration, statusCode, configuration.ConfigurationDistributor.DistributorFactory, item.Value.DistributorKey, item.Value.DistributorType, item.Value.PermissionType, item.Value.IsDurable, messageQueues, database);
                        messageDistributors.Add(item.Value.DistributorKey, dis);
                    }
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.6");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> CreateDefaultQueues()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = statusCode.GetStatusMessage("1.7.7");

            foreach (var item in configuration.ConfigurationQueue.DefaultQueueConfigDatas)
            {
                try
                {
                    if (messageQueues.Get(item.Key) == null)
                    {
                        var que = QueueFactory.Create<THub>(configuration, database, item.Value.QueueKey, item.Value.MatchType, item.Value.SendType, item.Value.PermissionType, item.Value.IsDurable, clientMasterData, messageMaster, database.Message, ref hubContext);
                        messageQueues.Add(item.Value.QueueKey, que);
                    }
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.8");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> QueueSetMessages()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            var errMsgList = "Add Message Error Id List : ";

            //todo kontrol et
            var messages = database.Message.GetAll(x => x.IsActive == true && x.IsDelete ==false && x.Status.StatusTypeMessage == StatusTypeMessageEnum.None);

            foreach (var msg in messages)
            {
                var queue = messageQueues.Get(msg.Message.Routing.QueueKey);
                var addResult = await queue.Queueing(msg);

                if (!addResult.IsSucces)
                {
                    result.IsSucces = false;
                    errMsgList += msg.Id + ",";
                    continue;
                }
            }

            if (result.IsSucces)
                result.Message = statusCode.GetStatusMessage("1.7.9");
            else
                result.Message = statusCode.GetStatusMessage("1.7.10") + " " + errMsgList;


            return result;
        }



        public async Task<ResponseModel> DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                if (distributor != null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.1");
                }
                else
                {
                    var newDistributor = DistributorFactory.Create(configuration, statusCode, configuration.ConfigurationDistributor.DistributorFactory, distributorKey, distributorType, PermissionTypeEnum.All, isDurable, messageQueues, database);
                    var resultDatabase = database.Distributor.Add(Guid.NewGuid(), new DistributorDbo
                    {
                        DistributorKey = distributorKey,
                        DistributorType = distributorType,
                        PermissionType = permissionType,
                        IsDurable = isDurable
                    });
                    var resultDistributorAdd = messageDistributors.Add(distributorKey, newDistributor);

                    if (resultDistributorAdd == true && resultDatabase == true)
                    {
                        result.IsSucces = true;
                        result.Message = statusCode.GetStatusMessage("1.7.2");
                    }
                    else
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.3");
                    }
                }
            });

            return result;
        }
        public async Task<ResponseModel> DistributorUpdate(string distributorKey, bool isDurable)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                if (distributor == null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.11");
                }
                else
                {
                    configuration.ConfigurationDistributor.DefaultDistributorConfigDatas.TryGetValue(distributorKey, out var defaultDistributor);
                    if (defaultDistributor != null)
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.12");
                    }
                    else
                    {
                        var getDatabaseDistributor = database.Distributor.Get(x => x.DistributorKey == distributorKey);
                        getDatabaseDistributor.IsDurable = isDurable;
                        var resultDatabase = database.Distributor.Update(getDatabaseDistributor.Id, getDatabaseDistributor);

                        distributor.IsDurable = isDurable;
                        var resultDistributorUpdate = messageDistributors.Update(distributorKey, distributor);

                        if (resultDistributorUpdate == true &&  resultDatabase == true)
                        {
                            result.IsSucces = true;
                            result.Message = statusCode.GetStatusMessage("1.7.13");
                        }
                        else
                        {
                            result.IsSucces = false;
                            result.Message = statusCode.GetStatusMessage("1.7.14");
                        }
                    }
                }
            });



            return result;
        }
        public async Task<ResponseModel> DistributorRemove(string distributorKey)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                if (distributor == null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.11");
                }
                else
                {
                    configuration.ConfigurationDistributor.DefaultDistributorConfigDatas.TryGetValue(distributorKey, out var defaultDistributor);
                    if (defaultDistributor != null)
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.15");
                    }
                    else
                    {
                        var checkBind = messageQueues.GetAll(x => x.DistributorKey == distributorKey);
                        if (checkBind != null && checkBind.Count > 0)
                        {
                            var errorMessage = ". Queue List : ";
                            foreach (var item in checkBind)
                            {
                                errorMessage += item.QueueKey + " , ";
                            }

                            result.IsSucces = false;
                            result.Message = statusCode.GetStatusMessage("1.7.16") + errorMessage;
                        }
                        else
                        {
                            var getDatabaseDistributor = database.Distributor.Get(x => x.DistributorKey == distributorKey);
                            var resultDatabase = database.Distributor.Delete(getDatabaseDistributor.Id, getDatabaseDistributor);

                            var resultDistributorRemove = messageDistributors.Remove(distributorKey);
                            if (resultDistributorRemove != null && resultDatabase == true)
                            {
                                result.IsSucces = true;
                                result.Message = statusCode.GetStatusMessage("1.7.17");
                            }
                            else
                            {
                                result.IsSucces = false;
                                result.Message = statusCode.GetStatusMessage("1.7.18");
                            }
                        }
                    }
                }
            });

            return result;
        }



        public async Task<ResponseModel> QueueCreate(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var queue = messageQueues.Get(queueKey);
                if (queue != null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.51");
                }
                else
                {
                    var resultDatabase = database.Queue.Add(Guid.NewGuid(), new QueueDbo
                    {
                        QueueKey= queueKey,
                        MatchType=matchType,
                        SendType=sendType,
                        PermissionType=permissionType,
                        IsDurable=isDurable
                    });

                    var newtQueue = QueueFactory.Create<THub>(
                    configuration,
                    database,
                    queueKey,
                    matchType,
                    sendType,
                    permissionType,
                    isDurable,
                    clientMasterData,
                    messageMaster,
                    database.Message,
                    ref hubContext);
                    var resultQueueAdd = messageQueues.Add(queueKey, newtQueue);

                    if (resultQueueAdd == true && resultDatabase == true)
                    {
                        result.IsSucces = true;
                        result.Message = statusCode.GetStatusMessage("1.7.52");
                    }
                    else
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.53");
                    }
                }
            });

            return result;
        }
        public async Task<ResponseModel> QueueUpdate(string queueKey, MatchTypeEnum matchType, SendTypeEnum sendType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var queue = messageQueues.Get(queueKey);
                if (queue == null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.54");
                }
                else
                {
                    configuration.ConfigurationQueue.DefaultQueueConfigDatas.TryGetValue(queueKey, out var defaultQueue);
                    if (defaultQueue != null)
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.55");
                    }
                    else
                    {
                        var getDatabaseQueue = database.Queue.Get(x => x.QueueKey == queueKey);
                        getDatabaseQueue.QueueKey = queueKey;
                        getDatabaseQueue.MatchType = matchType;
                        getDatabaseQueue.SendType = sendType;
                        getDatabaseQueue.PermissionType = permissionType;
                        getDatabaseQueue.IsDurable = isDurable;
                        var resultDatabase = database.Queue.Update(getDatabaseQueue.Id, getDatabaseQueue);

                        queue.MatchType = matchType;
                        queue.SendType = sendType;
                        queue.PermissionType = permissionType;
                        queue.IsDurable = isDurable;
                        var resultQueueUpdate = messageQueues.Update(queueKey, queue);

                        if (resultQueueUpdate == true &&  resultDatabase == true)
                        {
                            result.IsSucces = true;
                            result.Message = statusCode.GetStatusMessage("1.7.56");
                        }
                        else
                        {
                            result.IsSucces = false;
                            result.Message = statusCode.GetStatusMessage("1.7.57");
                        }
                    }
                }
            });



            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)

            return result;
        }
        public async Task<ResponseModel> QueueRemove(string queueKey)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var queue = messageQueues.Get(queueKey);
                if (queue == null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.54");
                }
                else
                {
                    configuration.ConfigurationQueue.DefaultQueueConfigDatas.TryGetValue(queueKey, out var defaultQueue);
                    if (defaultQueue != null)
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.58");
                    }
                    else
                    {
                        var checkMessage = queue.ChildMessageCount;
                        if (checkMessage > 0)
                        {
                            result.IsSucces = false;
                            result.Message = statusCode.GetStatusMessage("1.7.59");
                        }
                        else
                        {
                            var getDatabaseQueue = database.Queue.Get(x => x.QueueKey == queueKey);
                            var resultDatabase = database.Queue.Delete(getDatabaseQueue.Id, getDatabaseQueue);

                            var resultQueueRemove = messageQueues.Remove(queueKey);
                            if (resultQueueRemove != null && resultDatabase == true)
                            {
                                result.IsSucces = true;
                                result.Message = statusCode.GetStatusMessage("1.7.60");
                            }
                            else
                            {
                                result.IsSucces = false;
                                result.Message = statusCode.GetStatusMessage("1.7.61");
                            }
                        }
                    }
                }
            });

            return result;
        }
        public async Task<ResponseModel> QueueMerge(string distributorKey, string queueKey)
        {
            //todo default queue leri merge yaptırmamalıyım
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = messageDistributors.Get(distributorKey);
                var queue = messageQueues.Get(queueKey);
                if (distributor == null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.12");
                }
                if (queue == null)
                {
                    result.IsSucces = false;
                    result.Message = statusCode.GetStatusMessage("1.7.54");
                }
                else
                {
                    var getDatabaseQueue = database.Queue.Get(x => x.QueueKey == queueKey);
                    getDatabaseQueue.DistributorKey = distributorKey;
                    var resultDatabase = database.Queue.Update(getDatabaseQueue.Id, getDatabaseQueue);

                    queue.DistributorKey = distributorKey;
                    var resultQueueUpdate = messageQueues.Update(queueKey, queue);

                    if (resultQueueUpdate == true &&  resultDatabase == true)
                    {
                        result.IsSucces = true;
                        result.Message = statusCode.GetStatusMessage("1.7.62");
                    }
                    else
                    {
                        result.IsSucces = false;
                        result.Message = statusCode.GetStatusMessage("1.7.63");
                    }
                }
            });

            return result;
        }


       
        public async Task<ResponseModel> Brokering(MessageDbo message)
        {
            var result = new ResponseModel();

            var distributor = messageDistributors.Get(message.Message.Routing.DistributorKey);
            if (distributor != null)
                result = await distributor.Distributoring(message);
            else
            {
                result.IsOnline = true;
                result.IsSucces = false;
                result.Message = message.Message.Routing.DistributorKey + "  " + statusCode.GetStatusMessage("1.7.51");
            }


            


            //todo başka yere alınabilir
            if (result.IsQueue == false && result.IsSucces == false && message.Message.Routing.RoutingType == RoutingTypeEnum.Group && configuration.ConfigurationQueue.IsGroupQueueCreate == true)
            {
                QueueModel newQueue = DefaultQueueConst.NewClientGroupData;
                newQueue.QueueKey = message.Message.Routing.QueueKey;
                var resultNewQueue = await QueueCreate(newQueue.QueueKey, newQueue.MatchType, newQueue.SendType, newQueue.PermissionType, newQueue.IsDurable);

                if (resultNewQueue.IsSucces)
                {
                    result = await distributor.Distributoring(message);
                }
            }




            return result;
        }
        public async Task<ResponseModel> Brokering(List<MessageDbo> messages)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseModel> MessageAdd(MessageDbo message)
            => await messageDistributors.Get(message.Message.Routing.DistributorKey).Distributoring(message);
        public bool MessageAdd(List<MessageDbo> messages)
        {

            bool isError = false;
            foreach (var msg in messages)
            {
                var distributor = messageDistributors.Get(msg.Message.Routing.DistributorKey);
                var addResult = distributor.Distributoring(msg).Result;

                if (!addResult.IsSucces)
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
