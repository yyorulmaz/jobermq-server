using JoberMQ.Library.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Broker.Data
{
    internal class InMemoryBroker
    {
        internal static ConcurrentDictionary<Guid, MessageDbo> MasterMessages = new ConcurrentDictionary<Guid, MessageDbo>();
    }
}
