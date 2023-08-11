using System;
using System.Collections.Concurrent;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Data
{
    internal class MessageResultMemData
    {
        internal static ConcurrentDictionary<Guid, MessageResultDbo> MessageResultDatas = new ConcurrentDictionary<Guid, MessageResultDbo>();
    }
}
