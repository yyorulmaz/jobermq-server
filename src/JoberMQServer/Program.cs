using JoberMQ;
using JoberMQ.Hubs;
using JoberMQServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);

#region Linux
builder.Host.UseSystemd();
#endregion

builder.Host.ConfigureServices(services => JoberHost.ConfigureServices(services));

builder.WebHost.ConfigureKestrel(options =>
{
    //options.ListenAnyIP(7654); // HTTP için dinlenecek URL
    //options.ListenAnyIP(7655, listenOptions =>
    //{
    //    listenOptions.UseHttps(); // HTTPS için dinlenecek URL
    //});
    ////options.ListenAnyIP(5001, listenOptions =>
    ////{
    ////    listenOptions.UseHttps("path/to/certificate.pfx", "certificate-password"); // HTTPS için dinlenecek URL
    ////});
});

#region Worker
builder.Services.AddHostedService<Worker>();
//builder.Services.AddHostedService<Worker>(provider =>
//{
//    //var joberHub = app.Services.GetService<IHubContext<JoberHub>>();
//    return new Worker(builder.Services.BuildServiceProvider());
//});
#endregion

var app = builder.Build();
JoberHost.Configure(app);

Class1.serviceProvider =  app.Services;

app.Run();


