using JoberMQ.Business.Abstraction.Distributor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.InMemory
{
    internal class InMemoryMessageDistributor
    {
        internal static ConcurrentDictionary<string, IMessageDistributor> Datas = new ConcurrentDictionary<string, IMessageDistributor>();
    }
}
