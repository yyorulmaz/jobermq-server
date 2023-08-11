using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class SubscriptMemData
    {
        internal static ConcurrentDictionary<Guid, SubscriptDbo> SubscriptDatas = new ConcurrentDictionary<Guid, SubscriptDbo>();
    }
}
