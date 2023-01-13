using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Enums.Client;

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
    }
}
