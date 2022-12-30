using JoberMQ.Distributor.Abstraction;
using System.Collections.Concurrent;

namespace JoberMQ.Distributor.Data
{
    internal class InMemoryDistributor
    {
        internal static ConcurrentDictionary<string, IMessageDistributor> DistributorDatas = new ConcurrentDictionary<string, IMessageDistributor>();
    }
}
