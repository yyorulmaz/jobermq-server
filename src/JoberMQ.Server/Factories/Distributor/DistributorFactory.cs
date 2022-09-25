using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Implementation.Distributor.Default;

namespace JoberMQ.Server.Factories.Distributor
{
    internal class DistributorFactory
    {
        internal static IDistributor CreateDistributor(DistributorFactoryEnum distributorFactory, string distributorKey, DistributorTypeEnum type)
        {
            IDistributor distributor;

            switch (distributorFactory)
            {
                case DistributorFactoryEnum.Default:
                    switch (type)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfDistributorDirect(distributorKey, type);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfDistributorFilter(distributorKey, type);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfDistributorEvent(distributorKey, type);
                            break;
                        default:
                            distributor = new DfDistributorDirect(distributorKey, type);
                            break;
                    }
                    break;
                default:
                    switch (type)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfDistributorDirect(distributorKey, type);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfDistributorFilter(distributorKey, type);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfDistributorEvent(distributorKey, type);
                            break;
                        default:
                            distributor = new DfDistributorDirect(distributorKey, type);
                            break;
                    }
                    break;
            }

            return distributor;
        }
    }
}
