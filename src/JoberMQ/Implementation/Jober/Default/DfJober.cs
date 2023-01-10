using JoberMQ.Abstraction.Jober;
using JoberMQ.Broker.Abstraction;
using JoberMQ.Broker.Factories;
using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Data;
using JoberMQ.Common.Dbos;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Database.Factories;
using JoberMQ.Hubs;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Library.StatusCode.Factories;
using JoberMQ.Queue.Factories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Implementation.Jober.Default
{
    public class DfJober : IJober
    {
        public DfJober(IConfiguration configuration)
        {
            this.isJoberActive = configuration.ConfigurationJober.IsJoberActive;
            this.configuration = configuration;
        }

        #region Jober Active State
        bool isJoberActive;
        bool IJober.IsJoberActive { get => isJoberActive; set => isJoberActive = value; }
        #endregion
        
        #region Configuration
        IConfiguration configuration;
        IConfiguration IJober.Configuration => configuration;
        #endregion

        #region Status Code
        IStatusCode statusCode;
        IStatusCode IJober.StatusCode => statusCode;
        #endregion

        #region Client Master
        IMemRepository<string, IClient> clientMaster;
        IMemRepository<string, IClient> IJober.ClientMaster => clientMaster;
        #endregion

        #region Message Master
        IMemRepository<Guid, MessageDbo> messageMaster;
        IMemRepository<Guid, MessageDbo> IJober.MessageMaster => messageMaster;
        #endregion

        #region Database
        IDatabase database;
        IDatabase IJober.Database => database;
        #endregion

        #region Message Broker
        IMessageBroker messageBroker;
        IMessageBroker IJober.MessageBroker => messageBroker;
        #endregion




























        #region TimingService
        //ISchedule schedule;
        //ISchedule IJober.Schedule => schedule;
        #endregion



        #region JoberHubContext
        IHubContext<JoberHub> joberHubContext;
        IHubContext<JoberHub> IJober.JoberHubContext => joberHubContext;
        #endregion

        
        

        public async Task StartAsync()
        {
            this.statusCode = StatusCodeFactory.Create(configuration.ConfigurationStatusCode.StatusCodeFactory, configuration.ConfigurationStatusCode.StatusCodeDatas, configuration.ConfigurationStatusCode.StatusCodeMessageLanguage);
            this.clientMaster = JoberMQ.Library.Database.Factories.MemFactory.Create<string, IClient>(configuration.ConfigurationClient.ClientMasterFactory, configuration.ConfigurationClient.ClientMasterDataFactory, InMemoryClient.ClientMasterData);
            this.messageMaster = JoberMQ.Library.Database.Factories.MemFactory.Create<Guid, MessageDbo>(configuration.ConfigurationMessage.MessageMasterFactory, configuration.ConfigurationMessage.MessageMasterDataFactory, InMemoryMessage.MessageMasterData);
            this.database = DatabaseFactory.Create(configuration.ConfigurationDatabase);
            this.messageBroker =  MessageBrokerFactory.Create<JoberHub>(configuration, messageMaster, clientMaster, database, ref joberHubContext, ref isJoberActive);



            //this.schedule = ScheduleFactory.CreateSchedule(configuration.ConfigurationTiming.ScheduleFactory, databaseService);



            #region Schedule Job Start
            //var jobScheduleTimerStartResult = schedule.Start();
            //if (!jobScheduleTimerStartResult)
            //    throw new Exception(statusCode.GetStatusMessage("0.0.4"));
            //#endregion

            //#region Message Broker Start
            //var messageBrokerStartResult = messageBroker.Start();
            //#endregion

            //#region default user create
            //var userId = Guid.Parse("3b1fb872-c5de-40f4-8a93-342e754da53a");
            //var userCheck = databaseService.User.Get(userId);
            //if (userCheck == null)
            //{
            //    databaseService.User.Add(new UserDbo
            //    {
            //        Id = userId,
            //        UserName = "jobermq",
            //        Password = CryptionHashHelper.SHA256EnCryption("jobermq"),
            //        IsActive = true,
            //        IsDelete = false,
            //        DataStatusType = DataStatusTypeEnum.Insert
            //    });
            //}
            #endregion

            #region Server Start
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

            isJoberActive = true;

            JoberHost.Jober = this;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            #region Newtonsoft Json Protocol
            services
                .AddSignalR()
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
    }
}
