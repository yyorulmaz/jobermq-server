using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class EventSubMemData
    {
        internal static ConcurrentDictionary<Guid, EventSubDbo> EventSubDatas = new ConcurrentDictionary<Guid, EventSubDbo>();
    }
}
