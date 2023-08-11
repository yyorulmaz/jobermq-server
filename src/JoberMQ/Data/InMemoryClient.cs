using JoberMQ.Abstraction.Client;
using System.Collections.Concurrent;

namespace JoberMQ.Data
{
    internal class InMemoryClient
    {
        internal static ConcurrentDictionary<string, IClient> ClientMasterData = new ConcurrentDictionary<string, IClient>();
    }
}
