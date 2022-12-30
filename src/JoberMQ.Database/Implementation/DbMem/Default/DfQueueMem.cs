using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfQueueMem : DfMemRepository<Guid, QueueDbo>, IQueueDbMem
    {
        public DfQueueMem(ConcurrentDictionary<Guid, QueueDbo> data) : base(data)
        {
        }
    }
}
