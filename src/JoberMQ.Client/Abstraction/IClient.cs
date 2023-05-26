using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Models.Consume;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Abstraction
{
    public interface IClient
    {
        public string ConnectionId { get; }
        public string ClientKey { get; }

        public bool IsActive { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public int Number { get; set; }
        //ConcurrentDictionary<string, ConsumeModel> Consuming { get; set; }
        ConcurrentDictionary<Guid, ConsumeModel> Consuming { get; set; }
    }
}
