using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class JobMemData
    {
        internal static ConcurrentDictionary<Guid, JobDbo> JobDatas = new ConcurrentDictionary<Guid, JobDbo>();
    }
}
