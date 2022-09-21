using JoberMQNEW.Server.Abstraction.Client;
using System.Collections.Concurrent;

namespace JoberMQNEW.Server.Data
{
    internal class InMemoryClient
    {
        internal static ConcurrentDictionary<string, IClient> ClientDatas = new ConcurrentDictionary<string, IClient>();
        internal static ConcurrentDictionary<string, IClientGroup> ClientGroupDatas = new ConcurrentDictionary<string, IClientGroup>();
    }
}
