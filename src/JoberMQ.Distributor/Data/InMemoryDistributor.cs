using JoberMQ.Distributor.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Data
{
    internal class InMemoryDistributor
    {
        internal static ConcurrentDictionary<string, IMessageDistributor> MessageDistributorsData = new ConcurrentDictionary<string, IMessageDistributor>();
    }
}
