using JoberMQ.Broker.Abstraction;
using System.Collections.Concurrent;

namespace JoberMQ.Broker.Data
{
    internal class InMemoryDistributor
    {
        internal static ConcurrentDictionary<string, IMessageDistributor> DistributorDatas = new ConcurrentDictionary<string, IMessageDistributor>();
    }
}
