using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Database.MemoryData
{
    internal class JobMemData
    {
        internal static ConcurrentDictionary<Guid, JobDbo> JobDatas = new ConcurrentDictionary<Guid, JobDbo>();
    }
}
