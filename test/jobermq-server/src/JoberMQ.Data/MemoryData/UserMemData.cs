using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Data.MemoryData
{
    internal class UserMemData
    {
        internal static ConcurrentDictionary<Guid, UserDbo> UserDatas = new ConcurrentDictionary<Guid, UserDbo>();
    }
}
