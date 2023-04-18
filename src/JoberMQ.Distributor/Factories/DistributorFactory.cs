using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Database.Abstraction;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Implementation.Default;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Queue.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Factories
{
    internal class DistributorFactory
    {
        internal static IMessageDistributor Create(
            IConfiguration configuration,
            IStatusCode statusCode,
            DistributorFactoryEnum distributorFactory,
            string distributorKey,
            DistributorTypeEnum distributorType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IMemRepository<string, IMessageQueue> messageQueues,
            IDatabase database)
        {
            IMessageDistributor distributor;

            switch (distributorFactory)
            {
                case DistributorFactoryEnum.Default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfMessageDistributorDirect(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfMessageDistributorFilter(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfMessageDistributorEvent(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                        default:
                            distributor = new DfMessageDistributorDirect(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                    }
                    break;
                default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            distributor = new DfMessageDistributorDirect(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                        case DistributorTypeEnum.Filter:
                            distributor = new DfMessageDistributorFilter(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                        case DistributorTypeEnum.Event:
                            distributor = new DfMessageDistributorEvent(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                        default:
                            distributor = new DfMessageDistributorDirect(configuration,statusCode, distributorKey, distributorType, permissionType, isDurable, messageQueues, database);
                            break;
                    }
                    break;
            }

            return distributor;
        }
    }
}
