using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.MemoryData
{
    internal class EventSubMemData
    {
        internal static ConcurrentDictionary<Guid, EventSubDbo> EventSubDatas = new ConcurrentDictionary<Guid, EventSubDbo>();
    }
}
