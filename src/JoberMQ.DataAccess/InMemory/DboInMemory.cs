using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.InMemory
{
    internal class DboInMemory
    {
        internal static ConcurrentDictionary<Guid, UserDbo> UserDatas = new ConcurrentDictionary<Guid, UserDbo>();
        internal static ConcurrentDictionary<Guid, DistributorDbo> DistributorDatas = new ConcurrentDictionary<Guid, DistributorDbo>();
        internal static ConcurrentDictionary<Guid, QueueDbo> QueueDatas = new ConcurrentDictionary<Guid, QueueDbo>();
        internal static ConcurrentDictionary<Guid, EventSubDbo> EventSubDatas = new ConcurrentDictionary<Guid, EventSubDbo>();
        internal static ConcurrentDictionary<Guid, JobDataDbo> JobDataDboDatas = new ConcurrentDictionary<Guid, JobDataDbo>();
        internal static ConcurrentDictionary<Guid, JobDbo> JobDatas = new ConcurrentDictionary<Guid, JobDbo>();
        internal static ConcurrentDictionary<Guid, MessageDbo> MessageDatas = new ConcurrentDictionary<Guid, MessageDbo>();
        internal static ConcurrentDictionary<Guid, MessageResultDbo> MessageResultDatas = new ConcurrentDictionary<Guid, MessageResultDbo>();
    }
}
