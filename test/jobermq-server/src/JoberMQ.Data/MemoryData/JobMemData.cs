using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.MemoryData
{
    internal class JobMemData
    {
        internal static ConcurrentDictionary<Guid, JobDbo> Jobs = new ConcurrentDictionary<Guid, JobDbo>();
    }
}
