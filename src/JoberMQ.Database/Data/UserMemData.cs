using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class UserMemData
    {
        internal static ConcurrentDictionary<Guid, UserDbo> UserDatas = new ConcurrentDictionary<Guid, UserDbo>();
    }
}
