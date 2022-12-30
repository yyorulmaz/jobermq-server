using JoberMQ.Database.Abstraction.DbMem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Common.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Database.Implementation.DbMem.Default
{
    internal class DfEventSubMem : DfMemRepository<Guid, EventSubDbo>, IEventSubDbMem
    {
        public DfEventSubMem(ConcurrentDictionary<Guid, EventSubDbo> data) : base(data)
        {
        }
    }
}
