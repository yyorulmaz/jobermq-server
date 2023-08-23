using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Broker;
using JoberMQ.Abstraction.Client;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Database;
using JoberMQ.Abstraction.Distributor;
using JoberMQ.Abstraction.Queue;
using JoberMQ.Abstraction.Timing;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Helpers;
using JoberMQ.Common.Models;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Client;
using JoberMQ.Common.Models.Distributor;
using JoberMQ.Common.Models.Queue;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Rpc;
using JoberMQ.Common.StatusCode.Abstraction;
using JoberMQ.Common.StatusCode.Factories;
using JoberMQ.Data;
using JoberMQ.Factories.Broker;
using JoberMQ.Factories.Client;
using JoberMQ.Factories.Database;
using JoberMQ.Factories.Publisher;
using JoberMQ.Factories.Timing;
using JoberMQ.Hubs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Quartz;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;
using System.Transactions;

namespace JoberMQ.Implementation
{
    internal class DefaultJoberMQ : IJoberMQ
    {
        public DefaultJoberMQ(JoberMQ.Abstraction.Configuration.IConfiguration configuration)
        {
            this.configuration = configuration;
            this.statusCode = StatusCodeFactory.Create(configuration.ConfigurationStatusCode.StatusCodeFactory, configuration.ConfigurationStatusCode.StatusCodeDatas, configuration.ConfigurationStatusCode.StatusCodeMessageLanguage);
            this.clients = ClientsFactory.Create(configuration.ConfigurationClient.ClientsFactory, InMemoryClient.ClientMasterData);
            this.messageMasterData = MemFactory.Create<Guid, MessageDbo>(configuration.ConfigurationMessage.MessageMasterFactory, configuration.ConfigurationMessage.MessageMasterDataFactory, InMemoryMessage.MessageMasterData);
            this.database = DatabaseFactory.Create(configuration.ConfigurationDatabase);
            this.schedule = ScheduleFactory.CreateSchedule(configuration.ConfigurationTiming.ScheduleFactory);
            this.messageBroker = MessageBrokerFactory.Create(configuration.ConfigurationBroker.MessageBrokerFactory);
            this.distributors = MemFactory.Create<string, IMessageDistributor>(configuration.ConfigurationDistributor.DistributorsMemFactory, configuration.ConfigurationDistributor.DistributorsMemDataFactory);
            this.queues = MemFactory.Create<string, IMessageQueue>(configuration.ConfigurationQueue.QueuesMemFactory, configuration.ConfigurationQueue.QueuesMemDataFactory);

            this.scheduledNotifications = ScheduledNotificationsFactory.Create();
            JoberHost.JoberMQ = this;
        }

        JoberMQ.Abstraction.Configuration.IConfiguration configuration;
        JoberMQ.Abstraction.Configuration.IConfiguration IJoberMQ.Configuration => configuration;

        IStatusCode statusCode;
        IStatusCode IJoberMQ.StatusCode => statusCode;


        IClients clients;
        IClients IJoberMQ.Clients => clients;


        IHubContext<JoberHub> joberHubContext;
        //IHubContext<JoberHub> IJoberMQ.JoberHubContext => joberHubContext;
        public IHubContext<JoberHub> JoberHubContext { get => joberHubContext; set => joberHubContext = value; }


        IMemRepository<Guid, MessageDbo> messageMasterData;
        IMemRepository<Guid, MessageDbo> IJoberMQ.MessageMasterData => messageMasterData;


        IDatabase database;
        IDatabase IJoberMQ.Database => database;


        ISchedule schedule;
        ISchedule IJoberMQ.Schedule => schedule;


        IMessageBroker messageBroker;
        IMessageBroker IJoberMQ.MessageBroker => messageBroker;
        

        IMemRepository<string, IMessageDistributor> distributors;
        IMemRepository<string, IMessageDistributor> IJoberMQ.Distributors => distributors;



        IMemRepository<string, IMessageQueue> queues;
        IMemRepository<string, IMessageQueue> IJoberMQ.Queues => queues;


        IScheduledNotifications scheduledNotifications;
        IScheduledNotifications IJoberMQ.ScheduledNotifications => scheduledNotifications;


        async Task IJoberMQ.StartAsync(bool owinHost, IHubContext<JoberHub> hubContext = null)
        {
            #region Schedule Start
            var jobScheduleTimerStartResult = schedule.Start();
            if (!jobScheduleTimerStartResult)
                throw new Exception(statusCode.GetStatusMessage("0.0.4"));
            #endregion

            #region Database Setups
            // setups yapısı asenkron yapılacak
            database.Setups();
            #endregion

            #region Default User
            // buradaki yapıyı düzelt birde   Authority = "user" için ekle
            var userId = Guid.Parse("3b1fb872-c5de-40f4-8a93-342e754da53a");
            var userCheck = database.User.Get(userId);
            if (userCheck == null)
            {
                database.User.Add(userId, new UserDbo
                {
                    Id = userId,
                    UserName = "jobermq",
                    Authority = "administrators",
                    Password = CryptionHashHelper.SHA256EnCryption("jobermq"),
                    IsActive = true,
                    IsDelete = false,
                    DataStatusType = DataStatusTypeEnum.Insert
                });
            }
            #endregion

            #region Jober Host
            if (owinHost)
            {
                // todo url yapısını düzelt
                Uri urlHttp = new Uri($"http://{configuration.ConfigurationHost.HostName}:{configuration.ConfigurationHost.Port}");
                Uri urlHttps = new Uri($"https://{configuration.ConfigurationHost.HostName}:{configuration.ConfigurationHost.PortSsl}");

                var host = WebHost
                    .CreateDefaultBuilder()
                    .ConfigureServices(services => JoberHost.ConfigureServices(services))
                    .Configure(app => JoberHost.Configure(app, true))
                    .UseUrls(urlHttp.ToString(), urlHttps.ToString())
                    .Build();

                joberHubContext = host.Services.GetService<IHubContext<JoberHub>>();

                host.RunAsync();
            }
            else
            {
                joberHubContext = hubContext;
            }
            #endregion

            #region Message Broker
            var br1 = await messageBroker.ImportDatabaseDistributors();
            var br2 = await messageBroker.ImportDatabaseQueues();
            var br3 = await messageBroker.CreateDefaultDistributors();
            var br4 = await messageBroker.CreateDefaultQueues();
            var br5 = await messageBroker.QueueSetMessages();
            #endregion

            JoberHost.IsJoberActive = true;
            JoberHost.JoberMQ = this;

            scheduledNotifications.Start();
        }
        


        #region Connect
        async Task<bool> IJoberMQ.ConnectedOperationAsync(HubCallerContext context)
        {
            bool result = false;
            var clientInfoData = JsonConvert.DeserializeObject<ClientInfoDataModel>(context.GetHttpContext()?.Request.Headers["ClientInfoData"].ToString());
            var client = ClientFactory.Create(
                    configuration.ConfigurationClient.ClientFactory,
                    context.ConnectionId,
                    clientInfoData.ClientKey,
                    clientInfoData.ClientType);
            result = clients.Add(context.ConnectionId, client);

            if (result)
                await joberHubContext.Clients.Client(context.ConnectionId).SendCoreAsync("ReceiveServerActive", new object[] { JoberHost.IsJoberActive });

            return result;
        }
        async Task<bool> IJoberMQ.DisconnectedOperationAsync(HubCallerContext context)
        {
            return clients.Remove(context.ConnectionId) == null ? false : true;
        } 
        #endregion

        #region Distributor
        async Task<ResponseBaseModel<DistributorModel>> IJoberMQ.DistributorGetOperationAsync(string data)
        {
            var result = new ResponseBaseModel<DistributorModel>();
            result.IsOnline = true;
            result.Message = "";
            result.IsSucces = true;

            var distributor = distributors.Get(data);
            if (distributor == null)
                result.Data = null;

            result.Data = new DistributorModel
            {
                DistributorKey = distributor.DistributorKey,
                DistributorType = distributor.DistributorType,
                DistributorSearchSourceType = distributor.DistributorSearchSourceType,
                PermissionType = distributor.PermissionType,
                IsDurable = distributor.IsDurable,
                IsDefault = distributor.IsDefault
            };

            return result;
        }
        async Task<ResponseBaseModel> IJoberMQ.DistributorAddOperationAsync(DistributorModel data)
            => await messageBroker.DistributorAdd(data.DistributorKey, data.DistributorType.Value, data.DistributorSearchSourceType.Value, data.PermissionType.Value, data.IsDurable.Value);
        async Task<ResponseBaseModel> IJoberMQ.DistributorEditOperationAsync(DistributorModel data)
            => await messageBroker.DistributorUpdate(data.DistributorKey, data.PermissionType.Value, data.IsDurable.Value);
        async Task<ResponseBaseModel> IJoberMQ.DistributorRemoveOperationAsync(string data)
            => await messageBroker.DistributorRemove(data);
        #endregion

        #region Queue
        async Task<ResponseBaseModel<QueueModel>> IJoberMQ.QueueGetOperationAsync(string data)
        {
            var result = new ResponseBaseModel<QueueModel>();
            result.IsOnline = true;
            result.Message = "";
            result.IsSucces = true;

            var queue = queues.Get(data);
            if (queue == null)
                result.Data = null;

            result.Data = new QueueModel
            {
                QueueKey = queue.QueueKey,
                Tags = queue.Tags,
                QueueMatchType = queue.QueueMatchType,
                QueueOrderOfSendingType = queue.QueueOrderOfSendingType,
                PermissionType = queue.PermissionType,
                IsDurable = queue.IsDurable,
                IsDefault = queue.IsDefault,
                IsActive = queue.IsActive 
            };

            return result;
        }
        async Task<ResponseBaseModel<List<QueueModel>>> IJoberMQ.QueueGetAllOperationAsync(string data)
        {
            var result = new ResponseBaseModel<List<QueueModel>>();
            result.IsOnline = true;
            result.Message = "";
            result.IsSucces = true;
            result.Data = new List<QueueModel>();

            var queueAll = queues.GetAll();
            foreach (var item in queueAll)
            {
                var que = new QueueModel
                {
                    QueueKey = item.QueueKey,
                    Tags = item.Tags,
                    QueueMatchType = item.QueueMatchType,
                    QueueOrderOfSendingType = item.QueueOrderOfSendingType,
                    PermissionType = item.PermissionType,
                    IsDurable = item.IsDurable,
                    IsDefault = item.IsDefault,
                    IsActive = item.IsActive
                };

                result.Data.Add(que);
            }

            return result;
        }
        async Task<ResponseBaseModel> IJoberMQ.QueueAddOperationAsync(QueueModel data)
            => await messageBroker.QueueCreate(data.QueueKey, data.Tags, data.QueueMatchType.Value, data.QueueOrderOfSendingType.Value, data.PermissionType, data.IsDurable, data.IsActive);
        async Task<ResponseBaseModel> IJoberMQ.QueueEditOperationAsync(QueueModel data)
            => await messageBroker.QueueUpdate(data.QueueKey, data.Tags, data.PermissionType, data.IsDurable, data.IsActive);
        async Task<ResponseBaseModel> IJoberMQ.QueueRemoveOperationAsync(string data)
            => await messageBroker.QueueRemove(data);
        async Task<ResponseBaseModel> IJoberMQ.QueueBindOperationAsync(string data)
        {
            return null;
        }
        #endregion

        #region Consume
        async Task<ResponseBaseModel> IJoberMQ.ConsumeSubOperationAsync(string clientKey, string queueKey, bool isDurable)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;
            result.Message = "";
            result.IsSucces = true;

            var subCheck = database.Subscript.Get(x => x.ClientKey == clientKey && x.QueueKey == queueKey);

            if (subCheck == null)
            {
                var subscrriptDbo = new SubscriptDbo();
                subscrriptDbo.Id = Guid.NewGuid();
                subscrriptDbo.ClientKey = clientKey;
                subscrriptDbo.QueueKey = queueKey;
                subscrriptDbo.IsDurable = isDurable;
                database.Subscript.Add(subscrriptDbo.Id, subscrriptDbo);
            }
            else
            {
                subCheck.IsDurable = isDurable;
                database.Subscript.Update(subCheck.Id, subCheck);
            }

            return result;
        }
        async Task<ResponseBaseModel> IJoberMQ.ConsumeUnSubOperationAsync(string clientKey, string queueKey)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;
            result.Message = "";
            result.IsSucces = true;

            var subCheck = database.Subscript.Get(x => x.ClientKey == clientKey && x.QueueKey == queueKey);
            database.Subscript.Delete(subCheck.Id, subCheck);

            return result;
        }
        #endregion

        #region Message
        async Task<ResponseModel> IJoberMQ.MessageOperationAsync(MessageDbo data)
            => await distributors.Get(data.Message.Routing.DistributorKey).Distributoring(data);
        async Task<ResponseModel> IJoberMQ.JobOperationAsync(JobDbo data)
        {

            //var jobDeserialize = JsonConvert.DeserializeObject<JobDbo>(data);
            //var publisher = PublisherFactory.Create(configuration.ConfigurationPublisher.PublisherFactory, jobDeserialize.Publisher.PublisherType, configuration, database, schedule, messageBroker, statusCode);
            //var result = await publisher.Publish(jobDeserialize);

            var publisher = PublisherFactory.Create(configuration.ConfigurationPublisher.PublisherFactory, data.Publisher.PublisherType);
            return await publisher.Publish(data);
        }

        ConcurrentDictionary<Guid, Channel<RpcResponseModel>> channels = new ConcurrentDictionary<Guid, Channel<RpcResponseModel>>();
        async Task<RpcResponseModel> IJoberMQ.RpcMessageTextOperationAsync(Guid transactionId, string consumerKey, string message)
        {
            var response = new RpcResponseModel();
            response.Id = transactionId;

            await Task.Run(async () =>
            {
                IClient client = clients.Get(x => x.ClientKey == consumerKey);
                if (client == null)
                {
                    //todo return client yok
                }

                var channel = Channel.CreateUnbounded<RpcResponseModel>();
                //joberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveRpcMessageText", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                joberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveRpcMessageText", new object[] { response.Id, message }).ConfigureAwait(false);

                channels.TryAdd(transactionId, channel);


                int ssss = 0;
                while (await channel.Reader.WaitToReadAsync())
                {
                    while (channel.Reader.TryRead(out RpcResponseModel coordinates))
                    {
                        response = coordinates;

                        ssss = 1;
                        break;
                    }
                    if (ssss == 1)
                    {
                        break;
                    }
                }
            });


            channels.TryRemove(response.Id, out var dddd);
            return response;
        }
        async Task<RpcResponseModel> IJoberMQ.RpcMessageFunctionOperationAsync(Guid transactionId, string consumerKey, string message)
        {
            var response = new RpcResponseModel();
            response.Id = transactionId;

            await Task.Run(async () =>
            {
                IClient client = clients.Get(x => x.ClientKey == consumerKey);
                if (client == null)
                {
                    //todo return client yok
                }

                var channel = Channel.CreateUnbounded<RpcResponseModel>();
                //joberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveRpcMessageFunction", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                joberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveRpcMessageFunction", new object[] { response.Id, message }).ConfigureAwait(false);

                channels.TryAdd(transactionId, channel);


                int ssss = 0;
                while (await channel.Reader.WaitToReadAsync())
                {
                    while (channel.Reader.TryRead(out RpcResponseModel coordinates))
                    {
                        response = coordinates;

                        ssss = 1;
                        break;
                    }
                    if (ssss == 1)
                    {
                        break;
                    }
                }
            });


            channels.TryRemove(response.Id, out var dddd);
            return response;
        }
        async Task IJoberMQ.RpcMessageResponseOperationAsync(Guid transactionId, string resultData, bool isError, string errorMessage)
        {
            var response = new RpcResponseModel();
            response.Id = transactionId;
            response.ResultData = resultData;
            response.IsError = isError;
            response.ErrorMessage = errorMessage;

            var chnl = channels.TryGetValue(response.Id, out var channel);
            channel.Writer.WriteAsync(response);
        }
        #endregion

        #region Started Completed
        async Task<ResponseModel> IJoberMQ.StartedOperation(string data)
        {
            var messageStartedData = JsonConvert.DeserializeObject<StartedModel>(data);
            var result = new ResponseModel();
            result.IsOnline = true;

            result.Id= messageStartedData.MessageId;

            await Task.Run(() =>
            {
                try
                {
                    var message = database.Message.Get(messageStartedData.MessageId);

                    if (message.Status.StatusTypeMessage == StatusTypeMessageEnum.None || message.Status.StatusTypeMessage == StatusTypeMessageEnum.SendClient)
                    {
                        message.Status.StatusTypeMessage = StatusTypeMessageEnum.Started;
                        database.Message.Update(message.Id, message);
                    }

                    result.IsSucces = true;
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                }
            });
            return result;
        }
        async Task<ResponseBaseModel> IJoberMQ.CompletedOperation(string data)
        {
            #region null message return
            if (data == null || data == "")
                return new ResponseBaseModel { IsOnline = true, IsSucces = false };
            #endregion

            #region completedData Deserialize
            var completedData = JsonConvert.DeserializeObject<CompletedModel>(data);
            #endregion

            //IsDataProtection yapısı kurulur ise buraya kuracağım

            #region get data
            var messageDbo = database.Message.Get(completedData.MessageId);
            var jobJobTransactionDbo = database.JobTransaction.Get(messageDbo.CreatedJobTransactionId.Value);
            var jobDbo = database.Job.Get(messageDbo.CreatedJobId.Value);
            #endregion

            #region Message update
            messageDbo.Status.IsError = completedData.IsError;
            messageDbo.Status.StatusTypeMessage = StatusTypeMessageEnum.Completed;
            database.Message.Update(messageDbo.Id, messageDbo);
            #endregion

            var childBrothers = database.Message.GetAll(x => x.CreatedJobId == messageDbo.CreatedJobTransactionId);
            var childBrothersFullCompleted = childBrothers.Where(x => x.Status.StatusTypeMessage != StatusTypeMessageEnum.Completed).Count() > 0 ? false : true;
            var childBrothersFullError = childBrothers.Where(x => x.Status.IsError == true).Count() > 0 ? true : false;

            if (childBrothersFullCompleted == false)
                return new ResponseBaseModel { IsOnline = true, IsSucces = true };


            #region JobTransaction completed update
            jobJobTransactionDbo.Status.IsError = childBrothersFullError;
            jobJobTransactionDbo.Status.IsCompleted = true;
            database.JobTransaction.Update(jobJobTransactionDbo.Id, jobJobTransactionDbo);
            #endregion

            if (jobJobTransactionDbo.Timing.IsTrigger == false && jobJobTransactionDbo.Timing.TriggerGroupsId != null)
            {
                var triggerBrothers = database.JobTransaction.GetAll(x => x.Timing.TriggerJobId == jobJobTransactionDbo.Timing.TriggerJobId);
                var triggerBrothersCompleted = triggerBrothers.Where(x => x.Status.IsCompleted == false).Count() > 0 ? false : true;

                if (triggerBrothersCompleted == false)
                    return new ResponseBaseModel { IsOnline = true, IsSucces = true };
                else
                {

                }
            }

            bool isTriggerOperation = false;
            if (jobJobTransactionDbo.Timing.IsTrigger == true && childBrothersFullError == true)
            {
                if (jobJobTransactionDbo.Timing.ErrorWorkflowStop == true)
                {

                    return new ResponseBaseModel { IsOnline = true, IsSucces = true };
                }
                else
                    isTriggerOperation = true;
            }

            if (isTriggerOperation == true)
            {
                var triggerJobTransactionList = database.JobTransaction.GetAll(x => x.Timing.TriggerJobId == jobJobTransactionDbo.Id);
                foreach (var item in triggerJobTransactionList)
                {
                    // get JobTransactionDetail and JobTransactionDetail isactive
                    item.JobTransactionDetails = database.JobTransaction.Get(x => x.Id == item.Id).JobTransactionDetails;
                    foreach (var child in item.JobTransactionDetails)
                    {
                        child.IsActive = true;
                    }


                    // JobTransaction isactive
                    item.IsActive = true;
                    database.JobTransaction.Update(item.Id, item);

                    foreach (var child in item.JobTransactionDetails)
                    {
                        var childMessage = database.Message.GetAll(x => x.CreatedJobTransactionDetailId == child.Id).FirstOrDefault();

                        childMessage.IsActive = true;
                        database.Message.Update(childMessage.Id, childMessage);
                    }
                }
            }

            return new ResponseBaseModel { IsOnline = true, IsSucces = true };
        }
        #endregion

    }
}
