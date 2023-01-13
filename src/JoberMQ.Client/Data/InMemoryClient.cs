using JoberMQ.Client.Abstraction;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Data
{
    internal class InMemoryClient
    {
        internal static ConcurrentDictionary<string, IClient> ClientMasterData = new ConcurrentDictionary<string, IClient>();
    }
}
