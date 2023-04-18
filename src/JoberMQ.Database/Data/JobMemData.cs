using JoberMQ.Library.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class JobMemData
    {
        internal static ConcurrentDictionary<Guid, JobDbo> JobDatas = new ConcurrentDictionary<Guid, JobDbo>();
    }
}
