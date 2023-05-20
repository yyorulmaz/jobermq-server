using JoberMQ.Abstraction.Jober;
using JoberMQ.Broker.Abstraction;
using JoberMQ.Broker.Factories;
using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Data;
using JoberMQ.Client.Factories;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Data;
using JoberMQ.Database.Abstraction;
using JoberMQ.Database.Factories;
using JoberMQ.Hubs;
using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Jober;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Enums.Status;
using JoberMQ.Library.Helpers;
using JoberMQ.Library.Models.Client;
using JoberMQ.Library.Models.Consume;
using JoberMQ.Library.Models.Distributor;
using JoberMQ.Library.Models.Message;
using JoberMQ.Library.Models.Queue;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.Models.Rpc;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Library.StatusCode.Factories;
using JoberMQ.Publisher.Factories;
using JoberMQ.State;
using JoberMQ.Timing.Abstraction;
using JoberMQ.Timing.Factories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace JoberMQ.Implementation.Jober.Default
{
    public class DfJober : IJober
    {
        #region constructor
        public DfJober(IConfiguration configuration)
           => this.configuration = configuration;
        #endregion

        #region property helper
        //bool isJoberActive { get; set; }
        //public bool IsJoberActive { get => isJoberActive; set => isJoberActive = value; }

        readonly IConfiguration configuration;
        IConfiguration IJober.Configuration => configuration;


        IStatusCode statusCode;
        IStatusCode IJober.StatusCode => statusCode;


        IDatabase database;
        IDatabase IJober.Database => database;


        IClientMasterData clientMasterData;
        IClientMasterData IJober.ClientMasterData => clientMasterData;


        IMemRepository<Guid, MessageDbo> messageMasterData;
        IMemRepository<Guid, MessageDbo> IJober.MessageMasterData => messageMasterData;


        IMessageBroker messageBroker;
        IMessageBroker IJober.MessageBroker => messageBroker;


        ISchedule schedule;
        ISchedule IJober.Schedule => schedule;


        IHubContext<JoberHub> joberHubContext;
        IHubContext<JoberHub> IJober.JoberHubContext => joberHubContext;


        ConcurrentDictionary<Guid, Channel<RpcResponseModel>> channels;
        ConcurrentDictionary<Guid, Channel<RpcResponseModel>> IJober.Channels { get => channels; set => channels = value; }
        #endregion

        public async Task StartAsync()
        {
            this.channels = new ConcurrentDictionary<Guid, Channel<RpcResponseModel>>();
            this.statusCode = StatusCodeFactory.Create(configuration.ConfigurationStatusCode.StatusCodeFactory, configuration.ConfigurationStatusCode.StatusCodeDatas, configuration.ConfigurationStatusCode.StatusCodeMessageLanguage);
            this.database = DatabaseFactory.Create(configuration.ConfigurationDatabase);
            this.clientMasterData = ClientMasterDataFactory.Create(configuration.ConfigurationClient.ClientMasterDataFactory, InMemoryClient.ClientMasterDataDatabase);
            this.messageMasterData = MemFactory.Create<Guid, MessageDbo>(configuration.ConfigurationMessage.MessageMasterFactory, configuration.ConfigurationMessage.MessageMasterDataFactory, InMemoryMessage.MessageMasterData);


            this.schedule = ScheduleFactory.CreateSchedule(configuration.ConfigurationTiming.ScheduleFactory, database);
            var jobScheduleTimerStartResult = schedule.Start();
            if (!jobScheduleTimerStartResult)
                throw new Exception(statusCode.GetStatusMessage("0.0.4"));


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


            #region Jober Host
            // todo url yapısını düzelt
            Uri urlHttp = new Uri($"http://{configuration.ConfigurationHost.HostName}:{configuration.ConfigurationHost.Port}");
            Uri urlHttps = new Uri($"https://{configuration.ConfigurationHost.HostName}:{configuration.ConfigurationHost.PortSsl}");

            var host = WebHost
                .CreateDefaultBuilder()
                .ConfigureServices(services => ConfigureServices(services))
                .Configure(app => Configure(app))
                .UseUrls(urlHttp.ToString(), urlHttps.ToString())
                .Build();

            joberHubContext = host.Services.GetService<IHubContext<JoberHub>>();

            host.RunAsync();
            #endregion

            #region Message Broker
            this.messageBroker = MessageBrokerFactory.Create<JoberHub>(configuration, statusCode, messageMasterData, clientMasterData, database, ref joberHubContext);
            var br1 = await messageBroker.ImportDatabaseDistributors();
            var br2 = await messageBroker.ImportDatabaseQueues();
            var br3 = await messageBroker.CreateDefaultDistributors();
            var br4 = await messageBroker.CreateDefaultQueues();
            var br5 = await messageBroker.QueueSetMessages();
            #endregion


            JoberMQState.IsJoberActive =true;

            JoberHost.Jober = this;
        }
        private void ConfigureServices(IServiceCollection services)
        {
            #region Newtonsoft Json Protocol
            services
                .AddSignalR()
                //https://learn.microsoft.com/tr-tr/aspnet/core/signalr/messagepackhubprotocol?view=aspnetcore-7.0
                ////.AddMessagePackProtocol();
                //.AddMessagePackProtocol(options =>
                //{
                //    options.SerializerOptions = MessagePackSerializerOptions.Standard
                //        .WithResolver(new CustomResolver())
                //        .WithSecurity(MessagePackSecurity.UntrustedData);
                //});
                .AddNewtonsoftJsonProtocol(options =>
                {
                    options.PayloadSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            #endregion

            #region Quartz Disabled Log
            Quartz.Logging.LogProvider.IsDisabled = true;
            #endregion

            #region Authorization and Authentication
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                    {
                        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireClaim(ClaimTypes.NameIdentifier);
                    });

                });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateActor = false,
                            ValidateLifetime = false,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.ConfigurationSecurity.SecurityKey))
                        };


                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = ctx =>
                        {
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = ctx =>
                        {
                            return Task.CompletedTask;
                        },
                    };


                });
            #endregion

            #region Controllers
            services.AddControllers();
            #endregion
        }
        private void Configure(IApplicationBuilder app)
        {
            app.UseOwin();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<JoberHub>("/JoberHub");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }



        async Task<bool> IJober.ConnectedOperation(HubCallerContext context)
        {
            bool result = false;

            var clientInfoData = JsonConvert.DeserializeObject<ClientInfoDataModel>(context.GetHttpContext()?.Request.Headers["ClientInfoData"].ToString());

            await Task.Run(() =>
            {
                var client = ClientFactory.CreateClient(
                    JoberHost.Jober.Configuration.ConfigurationClient.ClientFactory,
                    context.ConnectionId,
                    clientInfoData.ClientKey,
                    clientInfoData.ClientGroupKey,
                    clientInfoData.ClientType);





                //// todo kontrol et. EventDbo tablosuna ClientGroupKey koymuştum onu kullanıyormuyum. 
                //var eventSubList = database.EventSub.GetAll(x => x.IsActive == true && x.IsDelete == false && x.ClientKey == clientInfoData.ClientKey).ToList();
                //foreach (var eventSub in eventSubList)
                //{
                //    var sub = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Event && x.Value.DeclareKey == eventSub.EventKey);
                //    if (sub == null || sub.Count() == 0)
                //    {
                //        var consume = new ConsumeModel();
                //        consume.ConsumeType = ConsumeTypeEnum.Event;
                //        consume.DeclareKey = eventSub.EventKey;
                //        client.Consuming.TryAdd(Guid.NewGuid(), consume);
                //    }
                //}



                result = clientMasterData.Add(context.ConnectionId, client);
            });


            QueueModel queue = DefaultQueueConst.NewClientGroupData;
            queue.QueueKey = clientInfoData.ClientGroupKey;
            await QueueOperation(queue);



            ////todo cluster
            //var highAvailabilities = Startup.ClientService.ClientData.GetAll(x => x.ClientType == ClientTypeEnum.HighAvailability || x.ClientType == ClientTypeEnum.LoadBalancingANDHighAvailability);
            //if (highAvailabilities == null || highAvailabilities.Count == 0)
            //    Startup.ServerService.IsHighAvailability = false;
            //else
            //    Startup.ServerService.IsHighAvailability = true;

            if (result)
                await joberHubContext.Clients.Client(context.ConnectionId).SendCoreAsync("ReceiveServerActive", new object[] { JoberMQState.IsJoberActive });

            return result;
        }
        async Task<bool> IJober.DisConnectedOperation(HubCallerContext context)
        {
            bool result = false;

            await Task.Run(() =>
            {
                result = clientMasterData.Remove(context.ConnectionId) == null ? false : true;
            });

            return result;
        }



        async Task<ResponseModel> IJober.DistributorOperation(string distributorData)
        {
            var result = new ResponseModel();
            var data = JsonConvert.DeserializeObject<DistributorModel>(distributorData);

            switch (data.DistributorOperationType)
            {
                case DistributorOperationTypeEnum.Create:
                    result = await JoberHost.Jober.MessageBroker.DistributorCreate(data.DistributorKey, data.DistributorType, data.PermissionType, data.IsDurable);
                    break;
                case DistributorOperationTypeEnum.Update:
                    result = await JoberHost.Jober.MessageBroker.DistributorUpdate(data.DistributorKey, data.IsDurable);
                    break;
                case DistributorOperationTypeEnum.Remove:
                    result = await JoberHost.Jober.MessageBroker.DistributorRemove(data.DistributorKey);
                    break;
            }

            return result;
        }
        async Task<ResponseModel> IJober.QueueOperation(string queueData)
        {
            var result = new ResponseModel();
            var data = JsonConvert.DeserializeObject<QueueModel>(queueData);
            return await QueueOperation(data);
        }
        async Task<ResponseModel> QueueOperation(QueueModel data)
        {
            var result = new ResponseModel();

            switch (data.QueueOperationType)
            {
                case QueueOperationTypeEnum.Create:
                    result = await JoberHost.Jober.MessageBroker.QueueCreate(data.QueueKey, data.MatchType, data.SendType, data.PermissionType, data.IsDurable);
                    break;
                case QueueOperationTypeEnum.Update:
                    result = await JoberHost.Jober.MessageBroker.QueueUpdate(data.QueueKey, data.MatchType, data.SendType, data.PermissionType, data.IsDurable);
                    break;
                case QueueOperationTypeEnum.Remove:
                    result = await JoberHost.Jober.MessageBroker.QueueRemove(data.QueueKey);
                    break;
                case QueueOperationTypeEnum.DistributorMerge:
                    result = await JoberHost.Jober.MessageBroker.QueueMerge(data.DistributorKey, data.QueueKey);
                    break;
            }

            return result;
        }
        async Task<ResponseModel> IJober.ConsumeOperation(string connectionId, string consumeData)
        {
            //#region eski
            //var result = new ResponseModel();
            //result.IsOnline = true;

            //await Task.Run(() =>
            //{
            //    var data = JsonConvert.DeserializeObject<ConcurrentDictionary<string, ConsumeModel>>(consumeData);
            //    var client = JoberHost.Jober.ClientMasterData.Get(connectionId);
            //    client.Consuming = data;
            //    result.IsSucces = JoberHost.Jober.ClientMasterData.Update(connectionId, client);
            //});

            //return result;
            //#endregion


            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var data = JsonConvert.DeserializeObject<ConsumeRequestModel>(consumeData);
                var client = JoberHost.Jober.ClientMasterData.Get(connectionId);
                client.Consuming = data.Consuming;


                // todo kontrol et. Tüm consume yapısını düzenlemeliyim. Bu sub, unsub işleri içime sinmedi
                // bu yapıyı her ConsumeType için ayrı ayrı yapmalıyım
                if (data.ConsumeOperationType == ConsumeOperationTypeEnum.EventSubscript)
                {
                    var eventSub = new EventSubDbo();
                    eventSub.Id = Guid.NewGuid();
                    eventSub.EventKey = data.DeclareKey;
                    eventSub.MatchType = MatchTypeEnum.Special;
                    eventSub.ClientKey = client.ClientKey;
                    eventSub.ClientGroupKey = client.ClientGroupKey;

                    var eventCheck = database.EventSub.Get(x => x.ClientKey == client.ClientKey && x.EventKey == data.DeclareKey);
                    if (eventCheck == null)
                        database.EventSub.Add(eventSub.Id, eventSub);
                    else
                    {
                        eventCheck.IsActive = true;
                        eventCheck.IsDelete = false;
                        database.EventSub.Update(eventCheck.Id, eventCheck);
                    }
                }
                else if (data.ConsumeOperationType == ConsumeOperationTypeEnum.EventUnSubscript)
                {
                    var eventCheck = database.EventSub.Get(x => x.ClientKey == client.ClientKey && x.EventKey == data.DeclareKey);
                    if (eventCheck != null)
                    {
                        database.EventSub.Delete(eventCheck.Id, eventCheck);
                    }
                }

                var eventSubList = database.EventSub.GetAll(x => x.IsActive == true && x.IsDelete == false && x.ClientKey == client.ClientKey).ToList();
                foreach (var eventSub in eventSubList)
                {
                    var sub = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Event && x.Value.DeclareKey == eventSub.EventKey);
                    if (sub == null || sub.Count() == 0)
                    {
                        var consume = new ConsumeModel();
                        consume.ConsumeType = ConsumeTypeEnum.Event;
                        consume.DeclareKey = eventSub.EventKey;
                        client.Consuming.TryAdd(Guid.NewGuid(), consume);
                    }
                }





                result.IsSucces = JoberHost.Jober.ClientMasterData.Update(connectionId, client);
            });

            return result;
        }




        async Task<ResponseModel> IJober.JobOperation(string job)
        {
            var jobDeserialize = JsonConvert.DeserializeObject<JobDbo>(job);
            var publisher = PublisherFactory.Create(configuration.ConfigurationPublisher.PublisherFactory, jobDeserialize.Publisher.PublisherType, configuration, database, schedule, messageBroker, statusCode);
            var result = await publisher.Publish(jobDeserialize);

            return result;
        }
        async Task<ResponseModel> IJober.MessageOperation(string message)
            => await messageBroker.MessageAdd(JsonConvert.DeserializeObject<MessageDbo>(message));

        async Task<RpcResponseModel> IJober.RpcOperation(string rpc)
        {
            var message = JsonConvert.DeserializeObject<RpcRequestModel>(rpc);
            var response = new RpcResponseModel();



            await Task.Run(async () =>
            {
                IClient client = clientMasterData.Get(x => x.ClientKey == message.ConsumerId);
                if (client == null)
                {
                    //todo return client yok
                }

                var channel = Channel.CreateUnbounded<RpcResponseModel>();
                joberHubContext.Clients.Client(client.ConnectionId).SendCoreAsync("ReceiveRpc", new[] { JsonConvert.SerializeObject(message) }).ConfigureAwait(false);
                
                channels.TryAdd(message.Id, channel);

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
        async Task IJober.RpcResponseOperation(string rpc)
        {
            var message = JsonConvert.DeserializeObject<RpcResponseModel>(rpc);

            var chnl = channels.TryGetValue(message.Id, out var channel);
            channel.Writer.WriteAsync(message);
        }


        async Task<ResponseModel> IJober.MessageStartedOperation(string data)
        {
            var messageStartedData = JsonConvert.DeserializeObject<MessageStartedModel>(data);
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
        async Task<ResponseModel> IJober.MessageCompletedOperation(string data)
        {
            var messageCompletedData = JsonConvert.DeserializeObject<MessageCompletedModel>(data);
            var result = new ResponseModel();

            return result;
        }

    }
}
