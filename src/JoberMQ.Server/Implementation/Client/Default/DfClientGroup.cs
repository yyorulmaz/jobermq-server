using JoberMQNEW.Server.Abstraction.Client;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace JoberMQ.Server.Implementation.Client.Default
{
    internal class DfClientGroup : IClientGroup
    {
        private readonly string groupName;
        private readonly ConcurrentDictionary<string, IClient> clientChilds;
        public DfClientGroup(string groupName)
        {
            clientChilds = new ConcurrentDictionary<string, IClient>();
            this.groupName = groupName;
        }
        public string GroupName => groupName;
        public ConcurrentDictionary<string, IClient> ClientChilds => clientChilds;
        public IClient Get(string clientKey)
        {
            ClientChilds.TryGetValue(clientKey, out IClient value);
            return value;
        }
        public IClient Get(Func<IClient, bool> filter)
        {
            return ClientChilds.Values.FirstOrDefault(filter);
        }
        public bool Add(IClient client) => Add(client.ClientKey, client);
        public bool Add(string clientKey, IClient client)
        {
            var result = ClientChilds.TryAdd(client.ClientKey, client);
            if (result)
                ChangedAdded?.Invoke(client);
            return result;
        }
        public bool Update(IClient client) => Update(client.ClientKey, client);
        public bool Update(string clientKey, IClient client)
        {
            var result = clientChilds.TryUpdate(clientKey, client, null);
            if (result)
                ChangedUpdated?.Invoke(client);
            return result;
        }
        public IClient Remove(string clientKey)
        {
            clientChilds.TryRemove(clientKey, out var client);
            if (client != null)
                ChangedRemoved?.Invoke(client);
            return client;
        }

        public event Action<IClient> ChangedAdded;
        public event Action<IClient> ChangedUpdated;
        public event Action<IClient> ChangedRemoved;
    }
}
