using System.Text.RegularExpressions;

internal class Programfff
{
    static void Main(string[] args)
    {

        List<QueueModel> queueModels = new List<QueueModel>()
            {
                new QueueModel
                {
                    QueueKey = "queue.default.special"
                },
                new QueueModel
                {
                    QueueKey = "queue.product"
                },
                new QueueModel
                {
                    QueueKey = "queue.basket"
                },
                new QueueModel
                {
                    QueueKey = "log"
                },
                new QueueModel
                {
                    QueueKey = "cache"
                },
                new QueueModel
                {
                    QueueKey = "queuecat"
                }
            };

        string startsWith = "queue";
        string contains = ".";
        string endsWith = "t";

        var dddddddddddd = queueModels.Where(x => x.QueueKey.StartsWith(startsWith) && x.QueueKey.Contains(contains) && x.QueueKey.EndsWith(endsWith)).ToList();

        Console.WriteLine("Hello World!");



        string text = "Bu bir örnek metindir.";
        string startsWithhhh = "Bu";

        Regex regex = new Regex($"^{Regex.Escape(startsWithhhh)}");
        bool starts = regex.IsMatch(text);


        



    }
}

public class QueueModel
{
    public string QueueKey { get; set; }
}

public static class SearchExtensions
{


}





/*
 
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text.RegularExpressions;


//ConcurrentDictionary<Guid, Person> MasterData = new ConcurrentDictionary<Guid, Person>();
//MasterData.TryAdd(Guid.NewGuid(), new Person { QueueKey = "queue.basket.sales.haydea" });
//MasterData.TryAdd(Guid.NewGuid(), new Person { QueueKey = "basket.sales.hayde" });
//MasterData.TryAdd(Guid.NewGuid(), new Person { QueueKey = "queue.sepet.mepet." });
//MasterData.TryAdd(Guid.NewGuid(), new Person { QueueKey = "bubirkuyruk" });
//MasterData.TryAdd(Guid.NewGuid(), new Person { QueueKey = "specialqueue" });


//Regex regex = new Regex("^q.*a$"); // Filtreleme için kullanılacak regex
//var filteredList = MasterData.Where(person => regex.IsMatch(person.Value.QueueKey)).ToList(); // Filtrelenmiş liste

////foreach (Person person in filteredList)
////{
////    Console.WriteLine(person.QueueKey); // Filtrelenmiş string değerleri yazdırma
////}










ConcurrentDictionary<Guid, MessageQueue> MasterData = new ConcurrentDictionary<Guid, MessageQueue>();
MasterData.TryAdd(Guid.NewGuid(), new MessageQueue { QueueKey = "queue.basket.sales.hayde" });
MasterData.TryAdd(Guid.NewGuid(), new MessageQueue { QueueKey = "queuebasket.sales.hayde" });
MasterData.TryAdd(Guid.NewGuid(), new MessageQueue { QueueKey = "queue.sepet.mepet." });
MasterData.TryAdd(Guid.NewGuid(), new MessageQueue { QueueKey = "bubirkuyruk" });
MasterData.TryAdd(Guid.NewGuid(), new MessageQueue { QueueKey = "specialqueue" });

//var regexPattern = CreateRegexPattern("queue", new string[] { "basket", "sales" }, "hayde");
//var regexPattern = CreateRegexPattern("queue", new string[] { "basket", "sales" }, null);
var regexPattern = CreateRegexPattern(null, new string[] { "i" }, null);
Regex regex = new Regex(regexPattern);

var icerenler = MasterData.Where(x => regex.IsMatch(x.Value.QueueKey)).ToList();







string CreateRegexPattern(string startsWith, string[] contains, string endsWith)
{
    string delimiter = "|";
    string containsValue = String.Join(delimiter, contains);

    //return  $@"\b{startsWith}\b.*\b({containsValue})\b.*\b{endsWith}\b";
    return $@"^{startsWith}(.*?)({containsValue})+.*?{endsWith}";
    
}



Console.WriteLine("Hello, World!");

public class MessageQueue
{
    public string QueueKey { get; set; }
    public int ChildMessageCount { get; }
}






public class Person
{
    public string QueueKey { get; set; }
}
 */