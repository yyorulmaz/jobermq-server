using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Broker.Abstraction;
using JoberMQ.Broker.Factories;
using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Factories;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Helpers;
using JoberMQ.Common.StatusCode.Abstraction;
using JoberMQ.Common.StatusCode.Factories;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Database.Factories;
using JoberMQ.Hubs;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Implementation.Jober.Default
{
    public class DfJober : IJober
    {
        public DfJober(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.statusCode = StatusCodeFactory.Create(Common.StatusCode.Enums.StatusCodeFactoryEnum.Default, configuration.StatusCodeDatas, Common.StatusCode.Enums.StatusCodeMessageLanguageEnum.tr);
            this.databaseService = DatabaseServiceFactory.CreateDatabaseService(configuration.ConfigurationDatabase);
            this.schedule = ScheduleFactory.CreateSchedule(configuration.ConfigurationTiming.ScheduleFactory, databaseService);
            this.clientService = ClientFactory.CreateClientService(configuration.ConfigurationClient);
            this.messageBroker = MessageBrokerFactory.Create<JoberHub>(
                configuration.ConfigurationBroker, configuration.ConfigurationDistributor, configuration.ConfigurationQueue, databaseService, clientService, joberHubContext);
        }

        #region Configuration
        IConfiguration configuration;
        //public IConfiguration Configuration { get => configuration; set => configuration = value; }
        IConfiguration IJober.Configuration => configuration;
        #endregion

        #region StatusCode
        IStatusCode statusCode;
        //IStatusCode IJober.StatusCode { get => statusCode; set => statusCode = value; }
        IStatusCode IJober.StatusCode => statusCode;
        #endregion

        #region DatabaseService
        IDatabaseService databaseService;
        //IDatabaseService IJober.DatabaseService { get => databaseService; set => databaseService = value; }
        IDatabaseService IJober.DatabaseService => databaseService;
        #endregion

        #region TimingService
        ISchedule schedule;
        ISchedule IJober.Schedule => schedule;
        #endregion

        #region ClientService
        IClientService clientService;
        IClientService IJober.ClientService => clientService;
        #endregion

        #region ClientService
        IMessageBroker messageBroker;
        IMessageBroker IJober.MessageBroker => messageBroker;
        #endregion

        #region JoberHubContext
        IHubContext<JoberHub> joberHubContext;
        IHubContext<JoberHub> IJober.JoberHubContext => joberHubContext;
        #endregion

        #region ServerActive
        bool isServerActive;
        bool IJober.IsServerActive { get => isServerActive; set => isServerActive = value; } 
        #endregion

        public async Task StartAsync()
        {
            #region Text Data Folder, File created
            var createDatabasesResult = databaseService.CreateDatabases();
            #endregion

            #region Text Data Group and Size
            var textDataDataClearAndSizeResult = databaseService.DataGroupingAndSizes();
            #endregion

            #region Completed Data Removes Timer Start
            var completedDataRemovesTimerStartResult = databaseService.CompletedDataRemovesTimerStart(configuration.ConfigurationDatabase.CompletedDataRemovesTimer);
            #endregion

            #region Text Data Import Memory
            var importTextDataToSetMemDbResult = databaseService.ImportTextDataToSetMemDb();
            #endregion

            #region Text Data Setup
            var textDataSetupResult = databaseService.Setups();
            #endregion

            #region Schedule Job Start
            var jobScheduleTimerStartResult = schedule.Start();
            if (!jobScheduleTimerStartResult)
                throw new Exception(statusCode.GetStatusMessage("0.0.4"));
            #endregion

            #region Message Broker Start
            var messageBrokerStartResult = messageBroker.Start();
            #endregion

            #region default user create
            var userId = Guid.Parse("3b1fb872-c5de-40f4-8a93-342e754da53a");
            var userCheck = databaseService.User.Get(userId);
            if (userCheck == null)
            {
                databaseService.User.Add(new UserDbo
                {
                    Id = userId,
                    UserName = "jobermq",
                    Password = CryptionHashHelper.SHA256EnCryption("jobermq"),
                    IsActive = true,
                    IsDelete = false,
                    DataStatusType = DataStatusTypeEnum.Insert
                });
            }

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

            isServerActive = true;

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
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecurityKey))
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
