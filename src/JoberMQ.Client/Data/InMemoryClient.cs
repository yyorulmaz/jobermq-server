using JoberMQ.Client.Abstraction;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Data
{
    internal class InMemoryClient
    {
        internal static ConcurrentDictionary<string, IClient> ClientMasterDataDatabase = new ConcurrentDictionary<string, IClient>();
    }
}


