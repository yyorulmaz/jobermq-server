using JoberMQ.Server;


IHost host = Host
    .CreateDefaultBuilder(args)
     // For Linux
     .UseSystemd()
    // For Windows
    // .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<WorkerJoberMQ>();
    })
    .Build();

host.Run();
