using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Abstraction
{
    internal interface IClient
    {
        public bool IsActive { get; }
        public ClientTypeEnum ClientType { get; }
        public string ConnectionId { get; }
        public string ClientKey { get; }
        public string ClientGroupKey { get; }
        public int RowNumber { get; set; }
    }
}
