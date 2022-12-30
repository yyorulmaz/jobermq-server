using System.Collections.Concurrent;

namespace JoberMQ.Client.Abstraction
{
    internal interface IClientService
    {
        public ConcurrentDictionary<string, IClient> Clients { get; }
        public ConcurrentDictionary<string, IClientGroup> ClientGroups { get; }
        public bool AddClient(IClient client);
        public bool AddClient(string clientKey, IClient client);
        public IClientGroup AddClientGroup(string groupName);
        public bool AddClientGroupChild(string groupName, IClient client);
        public bool UpdateClient(IClient client);
        public bool UpdateClient(string clientKey, IClient client);
        public bool UpdateClientAndChild(IClient client);
        public bool UpdateClientAndChild(string clientKey, IClient client);
        public bool RemoveClient(IClient client);
        public bool RemoveClient(string clientKey);
        public bool RemoveClientAndChild(IClient client);
        public bool RemoveClientAndChild(string clientKey);
    }
}
