using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Implementation.Distributor.Default;

namespace JoberMQ.Server.Factories.Distributor
{
    internal class DistributorFactory
    {
        internal static IDistributor CreateDistributor(DistributorFactoryEnum distributorFactory, string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable)
        {
            IDistributor distributor;

            switch (distributorFactory)
            {
                case DistributorFactoryEnum.Default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfDistributorDirect(distributorKey, distributorType, permissionType, isDurable);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfDistributorFilter(distributorKey, distributorType, permissionType, isDurable);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfDistributorEvent(distributorKey, distributorType, permissionType, isDurable);
                            break;
                        default:
                            distributor = new DfDistributorDirect(distributorKey, distributorType, permissionType, isDurable);
                            break;
                    }
                    break;
                default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfDistributorDirect(distributorKey, distributorType, permissionType, isDurable);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfDistributorFilter(distributorKey, distributorType, permissionType, isDurable);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfDistributorEvent(distributorKey, distributorType, permissionType, isDurable);
                            break;
                        default:
                            distributor = new DfDistributorDirect(distributorKey, distributorType, permissionType, isDurable);
                            break;
                    }
                    break;
            }

            return distributor;
        }
    }
}
