using JoberMQ;
using JoberMQ.Extensions;
using JoberMQ.Hubs;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Host.ConfigureServices(services => JoberHost.ConfigureServices(services));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();


JoberHost.Configure(app, false);

Task.Run(() =>
{
    var configuration = JoberHost.CreateConfiguration();
    configuration.IsOwinHost = false;

    var jober = JoberHost
        .CreateBuilder()
        .Configuration(configuration)
        .Build();

    jober.StartAsync(app.Services.GetService<IHubContext<JoberHub>>());
});

app.Run();

