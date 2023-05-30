using System;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Abstraction.Client
{
    public interface IClient : IDisposable
    {
        public string ConnectionId { get; }
        public string ClientKey { get; }
        public string[] Tags { get; set; }

        public bool IsActive { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public int Number { get; set; }
    }
}
