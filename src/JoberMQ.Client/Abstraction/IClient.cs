using JoberMQ.Library.Enums.Client;
using JoberMQ.Library.Models.Consume;
using System.Collections.Concurrent;

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
        ConcurrentDictionary<string, ConsumeModel> Consuming { get; set; }
    }
}
