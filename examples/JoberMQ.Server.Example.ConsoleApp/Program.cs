
var config = JoberMQ.Server.Factory.GetServerConfig();
var server = JoberMQ.Server.Factory.CreateServer(config);
server.Start();


Console.WriteLine("Hello, World!");
Console.ReadLine();
