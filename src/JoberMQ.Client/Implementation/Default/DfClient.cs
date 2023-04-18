using JoberMQ.Client.Abstraction;
using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Models.Consume;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Implementation.Default
{
    internal class DfClient : IClient
    {
        public DfClient(string connectionId, string clientKey, string clientGroupKey, ClientTypeEnum clientType)
        {
            this.connectionId = connectionId;
            this.clientKey = clientKey;
            this.clientGroupKey = clientGroupKey;
            this.clientType = clientType;

            this.consuming = new ConcurrentDictionary<string, ConsumeModel>();
        }

        readonly string connectionId;
        public string ConnectionId => connectionId;

        readonly string clientKey;
        public string ClientKey => clientKey;

        readonly string clientGroupKey;
        public string ClientGroupKey => clientGroupKey;

        bool isActive = false;
        public bool IsActive { get => isActive; set => isActive = value; }

        ClientTypeEnum clientType;
        public ClientTypeEnum ClientType { get => clientType; set => clientType = value; }

        int number;
        public int Number { get => number; set => number = value; }

        ConcurrentDictionary<string, ConsumeModel> consuming;
        public ConcurrentDictionary<string, ConsumeModel> Consuming { get => consuming; set => consuming = value; }
    }
}
