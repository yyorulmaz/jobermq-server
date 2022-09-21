using System;
using System.Collections.Concurrent;

namespace JoberMQNEW.Server.Abstraction.Client
{
    internal interface IClientGroup
    {
        public string GroupName { get; }
        public ConcurrentDictionary<string, IClient> ClientChilds { get; }
        public IClient Get(string clientKey);
        public IClient Get(Func<IClient, bool> filter);
        public bool Add(IClient client);
        public bool Add(string clientKey, IClient client);
        public bool Update(IClient client);
        public bool Update(string clientKey, IClient client);
        public IClient Remove(string clientKey);

        public event Action<IClient> ChangedAdded;
        public event Action<IClient> ChangedUpdated;
        public event Action<IClient> ChangedRemoved;
    }
}
