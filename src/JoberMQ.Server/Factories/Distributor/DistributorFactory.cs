using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Implementation.Distributor;

namespace JoberMQ.Server.Factories.Distributor
{
    internal class DistributorFactory
    {
        internal static IDistributor CreateDistributorService(DistributorFactoryEnum distributorFactory, string name, DistributorTypeEnum type)
        {
            IDistributor distributor;

            switch (distributorFactory)
            {
                case DistributorFactoryEnum.Default:
                    switch (type)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfDistributorDirect(name, type);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfDistributorFilter(name, type);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfDistributorEvent(name, type);
                            break;
                        default:
                            distributor = new DfDistributorDirect(name, type);
                            break;
                    }
                    break;
                default:
                    switch (type)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfDistributorDirect(name, type);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfDistributorFilter(name, type);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfDistributorEvent(name, type);
                            break;
                        default:
                            distributor = new DfDistributorDirect(name, type);
                            break;
                    }
                    break;
            }

            return distributor;
        }
    }
}
