using JoberMQ.Server.Abstraction.Distributor;
using System.Collections.Concurrent;

namespace JoberMQNEW.Server.Data
{
    internal class InMemoryDistributor
    {
        internal static ConcurrentDictionary<string, IDistributor> DistributorDatas = new ConcurrentDictionary<string, IDistributor>();
    }
}
