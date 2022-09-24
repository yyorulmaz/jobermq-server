using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Implementation.Queue.Default;

namespace JoberMQ.Server.Factories.Queue
{
    internal class QueueChildDataBaseFactory
    {
        internal static IQueueChildDataBasePriority CreateQueueChildDataBasePriority(QueueChildPriorityFactoryEnum queueChildPriorityFactory, IQueueDataBase queueDataBase)
        {
            IQueueChildDataBasePriority queueChildDataBase;

            switch (queueChildPriorityFactory)
            {
                case QueueChildPriorityFactoryEnum.Default:
                    queueChildDataBase = new DfQueueChildDataBasePriority(queueDataBase);
                    break;
                default:
                    queueChildDataBase = new DfQueueChildDataBasePriority(queueDataBase);
                    break;
            }

            return queueChildDataBase;
        }
        internal static IQueueChildDataBaseFIFO CreateQueueChildDataBaseFIFO(QueueChildFIFOFactoryEnum queueChildFIFOFactory, IQueueDataBase queueDataBase)
        {
            IQueueChildDataBaseFIFO queueChildDataBaseFIFO;

            switch (queueChildFIFOFactory)
            {
                case QueueChildFIFOFactoryEnum.Default:
                    queueChildDataBaseFIFO = new DfQueueChildDataBaseFIFO(queueDataBase);
                    break;
                default:
                    queueChildDataBaseFIFO = new DfQueueChildDataBaseFIFO(queueDataBase);
                    break;
            }

            return queueChildDataBaseFIFO;
        }
        internal static IQueueChildDataBaseLIFO CreateQueueChildDataBaseLIFO(QueueChildLIFOFactoryEnum queueChildLIFOFactory, IQueueDataBase queueDataBase)
        {
            IQueueChildDataBaseLIFO queueChildDataBaseLIFO;

            switch (queueChildLIFOFactory)
            {
                case QueueChildLIFOFactoryEnum.Default:
                    queueChildDataBaseLIFO = new DfQueueChildDataBaseLIFO(queueDataBase);
                    break;
                default:
                    queueChildDataBaseLIFO = new DfQueueChildDataBaseLIFO(queueDataBase);
                    break;
            }

            return queueChildDataBaseLIFO;
        }
    }
}
