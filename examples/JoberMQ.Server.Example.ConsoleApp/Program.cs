using JoberMQ;
using JoberMQ.Extensions;

#region 1. YÖNTEM
//var jober = JoberHost
//    .CreateBuilder()
//    .Build();

//jober.StartAsync();
#endregion

#region 2. YÖNTEM
var configuration = JoberHost.CreateConfiguration();
// edit configuration

var jober = JoberHost
    .CreateBuilder()
    .Configuration(configuration)
    .Build();

jober.StartAsync();
#endregion


Console.WriteLine("Hello, World!");
Console.ReadLine();

/*
 SignalR da bir örnek yapmanı istiyorum ve bunları c# ta yapacaksın
1. Producer client var
2. Consumer client var
3. Producer bir mesaj gönderdiğinde bunu server da bir System.Threading.Channels nesnesinde tutmak istiyorum
4. Sonra mesajı consumer 'a göndermek istiyorum
5. Consumer 'dan mesaj geldiğinde bunu channel ile takip etmek istiyorum
 
 
 */