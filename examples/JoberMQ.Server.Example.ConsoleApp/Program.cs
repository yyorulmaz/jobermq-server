//using System.Threading.Channels;

//var config = JoberMQ.Server.Factory.GetServerConfig();
//var server = JoberMQ.Server.Factory.CreateServer(config);
//server.Start();


//var startDate = DateTime.Now;
//for (int i = 0; i < 1000000; i++)
//{
//    //Console.WriteLine(i);
//}
//var endDate = DateTime.Now;

//Console.WriteLine(endDate - startDate);




#region CHANNEL YAPISI
//var channel = Channel.CreateUnbounded<int>();
////var channel = Channel.CreateBounded<string>(1);
////var channel = Channel.CreateUnbounded<byte>();



////Producer
//_ = Task.Run(async () =>
//{
//int number = 1;
//for (int i = 0; i < 10; i++)
//{
//await channel.Writer.WriteAsync(number);
//number++;
//await Task.Delay(1000);
//}
//});


////Consumer
//_ = Task.Run(async () =>
//{
//while (true)
//{

//var data = await channel.Reader.ReadAsync();
//Console.WriteLine($"Data:{data}");
////await Task.Delay(3000);
//}
//});

////Consumer
//_ = Task.Run(async () =>
//{
//while (true)
//{
//var data = await channel.Reader.ReadAsync();
//Console.WriteLine($"Data:{data} hhh");
////await Task.Delay(3000);
//}
//});

////Consumer
//_ = Task.Run(async () =>
//{
//while (true)
//{
//var data = await channel.Reader.ReadAsync();
//Console.WriteLine($"Data:{data} zzz");
////await Task.Delay(3000);
//}
//});

//Console.ReadLine();

//public class AAAAA
//{
//    static void Deneme()
//    {

//    }
//    static void Deneme(int aaaaa)
//    {

//    }
//    static int Deneme(int aaaaa, int bbbbb)
//    {
//        return 5;
//    }
//} 
#endregion







#region senaryo 1
////var jober = JoberMQ.JoberHost
////.CreateDefaultBuilder()
////.Build();

////jober.StartAsync(); 
#endregion


#region senaryo 2
using JoberMQ;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Extensions;
using System;

var configuration = JoberHost.CreateConfiguration();

var jober = JoberMQ.JoberHost
.CreateDefaultBuilder()
.Configuration(configuration)
.Build();


jober.StartAsync();
#endregion

Console.WriteLine("ok");
Console.ReadLine();

