using JoberMQ.Entities.Dbos;
using System.Collections.Concurrent;
using System;

namespace JoberMQ.Database.MemoryData
{
    internal class UserMemData
    {
        internal static ConcurrentDictionary<Guid, UserDbo> UserDatas = new ConcurrentDictionary<Guid, UserDbo>();

    }
}
