using JoberMQServer;

IHost host = Host.CreateDefaultBuilder(args)
    //For Linux
    .UseSystemd()
    // For Windows
    //.UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
