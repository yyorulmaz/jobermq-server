using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Queue;
using System;

namespace JoberMQ.Queue.Factories
{
    internal class QueueChildDataBaseFactory
    {
        internal static IChildMemGeneralRepository<Guid, MessageDbo> CreateQueueChildDataBasePriority(QueueChildPriorityFactoryEnum queueChildPriorityFactory, IMemRepository<Guid, MessageDbo> masterData)
        {
            IChildMemGeneralRepository<Guid, MessageDbo> queueChildDataBase;

            switch (queueChildPriorityFactory)
            {
                case QueueChildPriorityFactoryEnum.Default:
                    queueChildDataBase = new DfChildMemGeneralRepository<Guid, MessageDbo>(masterData);
                    break;
                default:
                    queueChildDataBase = new DfChildMemGeneralRepository<Guid, MessageDbo>(masterData);
                    break;
            }

            return queueChildDataBase;
        }
        internal static IChildMemFIFORepository<Guid, MessageDbo> CreateQueueChildDataBaseFIFO(QueueChildFIFOFactoryEnum queueChildFIFOFactory, IMemRepository<Guid, MessageDbo> masterData)
        {
            IChildMemFIFORepository<Guid, MessageDbo> queueChildDataBaseFIFO;

            switch (queueChildFIFOFactory)
            {
                case QueueChildFIFOFactoryEnum.Default:
                    queueChildDataBaseFIFO = new DfChildMemFIFORepository<Guid, MessageDbo>(masterData);
                    break;
                default:
                    queueChildDataBaseFIFO = new DfChildMemFIFORepository<Guid, MessageDbo>(masterData);
                    break;
            }

            return queueChildDataBaseFIFO;
        }
        internal static IChildMemLIFORepository<Guid, MessageDbo> CreateQueueChildDataBaseLIFO(QueueChildLIFOFactoryEnum queueChildLIFOFactory, IMemRepository<Guid, MessageDbo> masterData)
        {
            IChildMemLIFORepository<Guid, MessageDbo> queueChildDataBaseLIFO;

            switch (queueChildLIFOFactory)
            {
                case QueueChildLIFOFactoryEnum.Default:
                    queueChildDataBaseLIFO = new DfChildMemLIFORepository<Guid, MessageDbo>(masterData);
                    break;
                default:
                    queueChildDataBaseLIFO = new DfChildMemLIFORepository<Guid, MessageDbo>(masterData);
                    break;
            }

            return queueChildDataBaseLIFO;
        }
    }
}
