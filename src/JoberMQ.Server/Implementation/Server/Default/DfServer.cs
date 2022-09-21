using JoberMQ.Server.Abstraction.Config;
using JoberMQ.Server.Abstraction.Server;
using JoberMQ.Server.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Server.Implementation.Server.Default
{
    internal class DfServer: IServer
    {
        #region ServerConfig
        private readonly IServerConfig serverConfig;
        public IServerConfig ServerConfig => serverConfig;
        #endregion

        #region StatusCode
        private readonly IStatusCode statusCode;
        IStatusCode IServer.StatusCode => statusCode;
        #endregion

        public DfServer(IServerConfig serverConfig)
        {
            this.serverConfig = serverConfig;
            this.statusCode = StatusCodeFactory.CreateStatusCodeService(serverConfig.StatusCode.StatusCodeMessageLanguage);
        }

        public void Start()
        {
            #region StatusCode Start
            var statusCodeStartResult = statusCode.Start();
            if (!statusCodeStartResult)
                throw new Exception("error statusCodeStart");
            #endregion





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
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(serverConfig.Security.SecurityKey))
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
