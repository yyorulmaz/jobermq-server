using JoberMQ.Business.Abstraction.Queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.InMemory
{
    internal class InMemoryMessageQueue
    {
        internal static ConcurrentDictionary<string, IMessageQueue> Datas = new ConcurrentDictionary<string, IMessageQueue>();
    }
}
