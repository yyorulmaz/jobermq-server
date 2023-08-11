using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class UserMemData
    {
        internal static ConcurrentDictionary<Guid, UserDbo> UserDatas = new ConcurrentDictionary<Guid, UserDbo>();
    }
}
