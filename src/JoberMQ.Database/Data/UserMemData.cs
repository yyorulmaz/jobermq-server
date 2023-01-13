using JoberMQ.Common.Dbos;
using System.Collections.Concurrent;
using System;

namespace JoberMQ.Database.Data
{
    internal class UserMemData
    {
        internal static ConcurrentDictionary<Guid, UserDbo> UserDatas = new ConcurrentDictionary<Guid, UserDbo>();
    }
}
