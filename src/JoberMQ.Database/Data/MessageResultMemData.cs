using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Data
{
    internal class MessageResultMemData
    {
        internal static ConcurrentDictionary<Guid, MessageResultDbo> MessageResultDatas = new ConcurrentDictionary<Guid, MessageResultDbo>();
    }
}
