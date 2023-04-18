//using System.Threading.Channels;




//var channel = Channel.CreateUnbounded<int>();
////var channel = Channel.CreateBounded<string>(1);
////var channel = Channel.CreateUnbounded<byte>();



////Producer
//_ = Task.Run(async () =>
//{
//    int number = 1;
//    for (int i = 0; i < 10; i++)
//    {
//        await channel.Writer.WriteAsync(number);
//        number++;
//        await Task.Delay(1000);
//    }
//});


////Consumer
//_ = Task.Run(async () =>
//{
//    while (true)
//    {

//        var data = await channel.Reader.ReadAsync();
//        Console.WriteLine($"Data:{data}");
//        //await Task.Delay(3000);
//    }
//});

////Consumer
//_ = Task.Run(async () =>
//{
//    while (true)
//    {
//        var data = await channel.Reader.ReadAsync();
//        Console.WriteLine($"Data:{data} hhh");
//        //await Task.Delay(3000);
//    }
//});

////Consumer
//_ = Task.Run(async () =>
//{
//    while (true)
//    {
//        var data = await channel.Reader.ReadAsync();
//        Console.WriteLine($"Data:{data} zzz");
//        //await Task.Delay(3000);
//    }
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


//public static class ObservableExtensions
//{

//    public static ChannelReader<T> AsChannelReader<T>(
//        this IObservable<T> observable,
//        CancellationToken connectionAborted,
//        int? maxBufferSize = null
//    )
//    {
//        //IObserver<Channel<T>> observer = new IObserver<Channel<T>>();
//        //observer.OnCompleted();


//        var channel = maxBufferSize != null ? Channel.CreateBounded<T>(maxBufferSize.Value) : Channel.CreateUnbounded<T>();

//        var disposable = observable.Subscribe(
//                            value => channel.Writer.TryWrite(value),
//                            error => channel.Writer.TryComplete(error),
//                            () => channel.Writer.TryComplete());

//        var abortRegistration = connectionAborted.Register(() => channel.Writer.TryComplete());


//        channel.Reader.Completion.ContinueWith(task =>
//        {
//            disposable.Dispose();
//            abortRegistration.Dispose();
//        });

//        return channel.Reader;
//    }
//}