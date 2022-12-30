using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Factories;
using System.Collections.Concurrent;
using System.Linq;

namespace JoberMQ.Client.Implementation.Default
{
    internal class DfClientManager : IClientService
    {
        private readonly IConfigurationClient configurationClient;
        private readonly ConcurrentDictionary<string, IClient> clients;
        private readonly ConcurrentDictionary<string, IClientGroup> clientGroups;
        public DfClientManager(IConfigurationClient configurationClient, ConcurrentDictionary<string, IClient> clients, ConcurrentDictionary<string, IClientGroup> clientGroups)
        {
            this.configurationClient = configurationClient;
            this.clients = clients;
            this.clientGroups = clientGroups;
        }
        public ConcurrentDictionary<string, IClient> Clients => clients;
        public ConcurrentDictionary<string, IClientGroup> ClientGroups => clientGroups;


        public bool AddClient(IClient client) => AddClient(client.ClientKey, client);
        public bool AddClient(string clientKey, IClient client) => clients.TryAdd(clientKey, client);
        public IClientGroup AddClientGroup(string groupName)
        {
            var checkClientGroup = CheckGroup(groupName);
            if (checkClientGroup == null)
            {
                checkClientGroup = ClientFactory.CreateClientGroup(configurationClient.ClientGroupFactory, groupName);
                clientGroups.TryAdd(groupName, checkClientGroup);
            }

            return checkClientGroup;
        }
        public bool AddClientGroupChild(string groupName, IClient client)
        {
            return clientGroups.Where(x => x.Key == groupName).FirstOrDefault().Value.Add(client);
        }


        public bool UpdateClient(IClient client) => UpdateClient(client.ClientKey, client);
        public bool UpdateClient(string clientKey, IClient client)
        {
            return clients.TryUpdate(clientKey, client, null);
        }
        public bool UpdateClientAndChild(IClient client) => UpdateClientAndChild(client.ClientKey, client);
        public bool UpdateClientAndChild(string clientKey, IClient client)
        {
            foreach (var item in clientGroups.Values.ToList())
                item.Update(client);

            return clients.TryUpdate(clientKey, client, null);
        }


        public bool RemoveClient(IClient client) => RemoveClient(client.ClientKey);
        public bool RemoveClient(string clientKey) => clients.TryRemove(clientKey, out var vvvvv);
        public bool RemoveClientAndChild(IClient client) => RemoveClientAndChild(client.ClientKey);
        public bool RemoveClientAndChild(string clientKey)
        {
            foreach (var item in clientGroups.Values.ToList())
                item.Remove(clientKey);

            return clients.TryRemove(clientKey, out var xxxxx);
        }


        private IClientGroup CheckGroup(string groupName)
        {
            var checkGroup = ClientGroups.FirstOrDefault(x => x.Key == groupName);
            if (checkGroup.Key == null)
                return null;
            else
                return checkGroup.Value;
        }
    }
}
