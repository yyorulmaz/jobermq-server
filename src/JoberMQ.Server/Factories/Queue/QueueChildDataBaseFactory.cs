using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Implementation.Queue.Default;

namespace JoberMQ.Server.Factories.Queue
{
    internal class QueueChildDataBaseFactory
    {
        internal static IDbChildPriority CreateQueueChildDataBasePriority(QueueChildPriorityFactoryEnum queueChildPriorityFactory, IDb queueDataBase)
        {
            IDbChildPriority queueChildDataBase;

            switch (queueChildPriorityFactory)
            {
                case QueueChildPriorityFactoryEnum.Default:
                    queueChildDataBase = new DfDbChildPriority(queueDataBase);
                    break;
                default:
                    queueChildDataBase = new DfDbChildPriority(queueDataBase);
                    break;
            }

            return queueChildDataBase;
        }
        internal static IDbChildFIFO CreateQueueChildDataBaseFIFO(QueueChildFIFOFactoryEnum queueChildFIFOFactory, IDb queueDataBase)
        {
            IDbChildFIFO queueChildDataBaseFIFO;

            switch (queueChildFIFOFactory)
            {
                case QueueChildFIFOFactoryEnum.Default:
                    queueChildDataBaseFIFO = new DfDbChildFIFO(queueDataBase);
                    break;
                default:
                    queueChildDataBaseFIFO = new DfDbChildFIFO(queueDataBase);
                    break;
            }

            return queueChildDataBaseFIFO;
        }
        internal static IDbChildLIFO CreateQueueChildDataBaseLIFO(QueueChildLIFOFactoryEnum queueChildLIFOFactory, IDb queueDataBase)
        {
            IDbChildLIFO queueChildDataBaseLIFO;

            switch (queueChildLIFOFactory)
            {
                case QueueChildLIFOFactoryEnum.Default:
                    queueChildDataBaseLIFO = new DfDbChildLIFO(queueDataBase);
                    break;
                default:
                    queueChildDataBaseLIFO = new DfDbChildLIFO(queueDataBase);
                    break;
            }

            return queueChildDataBaseLIFO;
        }
    }
}
