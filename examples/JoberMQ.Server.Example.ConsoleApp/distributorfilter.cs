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