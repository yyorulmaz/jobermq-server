using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Implementation.Default;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Queue.Abstraction;

namespace JoberMQ.Distributor.Factories
{
    internal class DistributorFactory
    {
        internal static IMessageDistributor CreateDistributor(
            DistributorFactoryEnum distributorFactory, 
            string distributorKey, 
            DistributorTypeEnum distributorType, 
            PermissionTypeEnum permissionType, 
            bool isDurable, 
            IMemRepository<string, IMessageQueue> messageQueues)
        {
            IMessageDistributor distributor;

            switch (distributorFactory)
            {
                case DistributorFactoryEnum.Default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfMessageDistributorDirect(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfMessageDistributorFilter(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfMessageDistributorEvent(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                        default:
                            distributor = new DfMessageDistributorDirect(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                    }
                    break;
                default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfMessageDistributorDirect(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfMessageDistributorFilter(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfMessageDistributorEvent(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                        default:
                            distributor = new DfMessageDistributorDirect(distributorKey, distributorType, permissionType, isDurable, messageQueues);
                            break;
                    }
                    break;
            }

            return distributor;
        }
    }
}
