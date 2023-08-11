using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;
using JoberMQ.Factories.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using JoberMQ.Hubs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JoberMQ
{
    public class JoberHost
    {
        public static IJoberMQ JoberMQ { get; set; }
        public static bool IsJoberActive { get; set; }

        public static JoberMQ.Abstraction.Configuration.IConfiguration CreateConfiguration(ConfigurationFactoryEnum configurationFactory = ConfigurationConst.ConfigurationFactory)
            => ConfigurationFactory.Create(configurationFactory);

        public static JoberHostBuilder CreateBuilder()
            => new JoberHostBuilder();

        public static void ConfigureServices(IServiceCollection services)
        {
            #region Newtonsoft Json Protocol
            services
                .AddSignalR()
                .AddHubOptions<JoberHub>(options =>
                {
                    //options.MaximumParallelInvocationsPerClient = 256;
                    options.EnableDetailedErrors = true;
                    options.StreamBufferCapacity = 4096;

                    options.MaximumParallelInvocationsPerClient = 4096; // Yeni maksimum değer
                    options.MaximumReceiveMessageSize = 1024000; // Yeni maksimum mesaj boyutu
                    options.ClientTimeoutInterval = TimeSpan.FromSeconds(120); // Yeni istemci zaman aşımı süresi
                })
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
                            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.ConfigurationSecurity.SecurityKey))
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("böyle_bir_aşk_görülmemiş_dünyada_ne_geçmişte_nede_bundan_sonrada"))
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
        public static void Configure(IApplicationBuilder app, bool owinHost)
        {
            if (owinHost)
                app.UseOwin();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<JoberHub>("/JoberHub", options =>
                {
                    options.ApplicationMaxBufferSize = 4096;
                    options.TransportMaxBufferSize = 4096;
                });
                endpoints.MapHub<TestHub>("/TestHub");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
