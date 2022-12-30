using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Implementation.Queue;
using JoberMQNEW.Server.Data;

namespace JoberMQ.Server.Factories.Queue
{
    internal class QueueDataBaseFactory
    {
        internal static IDb GetQueueDataBase()
            => new DfDb(InMemoryQueue.QueueDataBase);
    }
}
