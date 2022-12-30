using JoberMQ.Client.Abstraction;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Data
{
    internal class InMemoryClient
    {
        internal static ConcurrentDictionary<string, IClient> ClientDatas = new ConcurrentDictionary<string, IClient>();
        internal static ConcurrentDictionary<string, IClientGroup> ClientGroupDatas = new ConcurrentDictionary<string, IClientGroup>();
    }
}
