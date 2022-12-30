using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Common.Dbos;
using JoberMQ.Queue.Data;
using System;

namespace JoberMQ.Queue.Factories
{
    internal class QueueDataBaseFactory
    {
        internal static IMemRepository<Guid, MessageDbo> GetQueueDataBase()
            => new DfMemRepository<Guid, MessageDbo>(InMemoryQueue.QueueDataBase);
    }
}
