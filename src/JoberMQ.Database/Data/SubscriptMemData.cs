using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class SubscriptMemData
    {
        internal static ConcurrentDictionary<Guid, SubscriptDbo> SubscriptDatas = new ConcurrentDictionary<Guid, SubscriptDbo>();
    }
}
