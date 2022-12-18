using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Server;
using JoberMQ.Server.Abstraction.Timing;
using JoberMQ.Server.Factories.Broker;
using JoberMQ.Server.Factories.Client;
using JoberMQ.Server.Factories.DbOpr;
using JoberMQ.Server.Factories.Timing;
using JoberMQ.Server.Helpers;
using JoberMQ.Server.Hubs;
using JoberMQNEW.Server.Abstraction.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Server.Implementation.Server.Default
{
    internal class DfServer : IServer
    {
        bool isServerActive;
        bool IServer.IsServerActive { get => isServerActive; set => isServerActive = value; }

        #region ServerConfig
        private readonly ServerConfigModel serverConfig;
        public ServerConfigModel ServerConfig => serverConfig;
        #endregion

        #region StatusCode
        private readonly IStatusCode statusCode;
        IStatusCode IServer.StatusCode => statusCode;
        #endregion

        #region DbOprService
        private readonly IDbOprService dbOprService;
        IDbOprService IServer.DbOprService => dbOprService;
        #endregion

        #region DboCreator
        private readonly IDboCreator dboCreator;
        IDboCreator IServer.DboCreator => dboCreator;
        #endregion

        #region ClientService
        private readonly IClientService clientService;
        IClientService IServer.ClientService => clientService;
        #endregion

        #region Schedule
        private readonly ISchedule schedule;
        ISchedule IServer.Schedule => schedule;
        #endregion

        #region Broker
        private readonly IBroker broker;
        IBroker IServer.Broker => broker;
        #endregion

        #region JoberHubContext
        private IHubContext<JoberHub> joberHubContext;
        IHubContext<JoberHub> IServer.JoberHubContext => joberHubContext;
        #endregion

        public DfServer(ServerConfigModel serverConfig)
        {
            this.serverConfig = serverConfig;
            this.statusCode = StatusCodeFactory.CreateStatusCodeService(serverConfig.StatusCodeConfig.StatusCodeMessageLanguage);
            this.dbOprService = DbOprServiceFactory.CreateDbOprService(serverConfig.DbOprConfig);
            this.dboCreator = DboCreatorFactory.CreateDboCreator(serverConfig.DbOprConfig.DboCreatorFactory, dbOprService);
            this.clientService = ClientFactory.CreateClientService(serverConfig);
            this.schedule = ScheduleFactory.CreateSchedule(serverConfig.TimingConfig.ScheduleFactory, dbOprService, dboCreator);
            this.broker = BrokerFactory.CreateBroker(serverConfig, dbOprService,clientService);
        }

        public void Start()
        {
            #region StatusCode Start
            var statusCodeStartResult = statusCode.Start();
            if (!statusCodeStartResult)
                throw new Exception("error statusCodeStart");
            #endregion

            #region Text Data Folder, File created
            var createDatabasesResult = dbOprService.CreateDatabases();
            #endregion

            #region Text Data Group and Size
            var textDataDataClearAndSizeResult = dbOprService.DataGroupingAndSizes();
            #endregion

            #region Completed Data Removes Timer Start
            var completedDataRemovesTimerStartResult = dbOprService.CompletedDataRemovesTimerStart(serverConfig.DbOprConfig.CompletedDataRemovesTimer);
            #endregion

            #region Text Data Import Memory
            var importTextDataToSetMemDbResult = dbOprService.ImportTextDataToSetMemDb();
            #endregion

            #region Text Data Folder, File created
            var textDataSetupResult = dbOprService.Setups();
            #endregion

            #region Schedule Job Start
            var jobScheduleTimerStartResult = schedule.Start();
            if (!jobScheduleTimerStartResult)
                throw new Exception(statusCode.GetStatusMessage("0.0.4"));
            #endregion

            #region Broker Start
            var brokerStartResult = broker.Start(); 
            #endregion



            #region default user create
            var userId = Guid.Parse("3b1fb872-c5de-40f4-8a93-342e754da53a");
            var userCheck = dbOprService.User.Get(userId);
            if (userCheck == null)
            {
                dbOprService.User.Add(new UserDbo
                {
                    Id = userId,
                    UserName = "jobermq",
                    Password = CryptionHashHelper.SHA256EnCryption("jobermq"),
                    IsActive = true,
                    IsDelete = false,
                    DataStatusType = Entities.Enums.Data.DataStatusTypeEnum.Insert
                });
            }

            #endregion

            #region Server Start
            // todo url yapısını düzelt
            Uri urlHttp = new Uri($"http://{serverConfig.HostConfig.HostName}:{serverConfig.HostConfig.Port}");
            Uri urlHttps = new Uri($"https://{serverConfig.HostConfig.HostName}:{serverConfig.HostConfig.PortSsl}");

            var host = WebHost
                .CreateDefaultBuilder()
                .ConfigureServices(services => ConfigureServices(services))
                .Configure(app => Configure(app))
                .UseUrls(urlHttp.ToString(), urlHttps.ToString())
                .Build();

            joberHubContext = host.Services.GetService<IHubContext<JoberHub>>();

            host.RunAsync();
            #endregion

            isServerActive = true;
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
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(serverConfig.SecurityConfig.SecurityKey))
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
