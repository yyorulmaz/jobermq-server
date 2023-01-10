using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Abstraction
{
    public interface IClient
    {
        public string ConnectionId { get; }
        public string ClientKey { get; }
        public string ClientGroupKey { get; }

        public bool IsActive { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public int Number { get; set; }
    }
}
