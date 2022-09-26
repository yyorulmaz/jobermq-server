
var config = JoberMQ.Server.Factory.GetServerConfig();
var server = JoberMQ.Server.Factory.CreateServer(config);
server.Start();


//var startDate = DateTime.Now;
//for (int i = 0; i < 1000000; i++)
//{
//    //Console.WriteLine(i);
//}
//var endDate = DateTime.Now;


//Console.WriteLine(endDate - startDate);
Console.ReadLine();
