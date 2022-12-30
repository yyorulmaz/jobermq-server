using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Implementation.Default
{
    internal class DfClient : IClient
    {
        private readonly bool isActive;
        private readonly ClientTypeEnum clientType;
        private readonly string connectionId;
        private readonly string clientKey;
        private readonly string clientGroupKey;

        public int rowNumber;
        public DfClient(ClientTypeEnum clientType, string connectionId, string clientKey, string clientGroupKey)
        {
            isActive = isActive = true;
            this.clientType = clientType;
            this.connectionId = connectionId;
            this.clientKey = clientKey;
            this.clientGroupKey = clientGroupKey;
        }
        public bool IsActive => isActive;
        public ClientTypeEnum ClientType => clientType;
        public string ConnectionId => connectionId;
        public string ClientKey => clientKey;
        public string ClientGroupKey => clientGroupKey;
        public int RowNumber { get { return rowNumber; } set { rowNumber = value; } }
    }
}
