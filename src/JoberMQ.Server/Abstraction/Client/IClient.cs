using JoberMQ.Entities.Enums.Client;

namespace JoberMQNEW.Server.Abstraction.Client
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
