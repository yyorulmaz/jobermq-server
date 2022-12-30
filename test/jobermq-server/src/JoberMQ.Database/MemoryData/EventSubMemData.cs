using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Database.MemoryData
{
    internal class EventSubMemData
    {
        internal static ConcurrentDictionary<Guid, EventSubDbo> EventSubDatas = new ConcurrentDictionary<Guid, EventSubDbo>();
    }
}
