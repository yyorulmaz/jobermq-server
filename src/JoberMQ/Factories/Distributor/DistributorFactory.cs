using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Abstraction.Distributor;
using JoberMQ.Implementation.Distributor.Default;

namespace JoberMQ.Factories.Distributor
{
    internal class DistributorFactory
    {
        public static IMessageDistributor Create(
            DistributorFactoryEnum distributorFactory, 
            
            string distributorKey, 
            DistributorTypeEnum distributorType, 
            DistributorSearchSourceTypeEnum distributorSearchSourceType, 
            PermissionTypeEnum permissionType, 
            bool isDurable, 
            bool isDefault)
        {
            IMessageDistributor result;

            switch (distributorFactory)
            {
                case DistributorFactoryEnum.Default:
                    switch (distributorType)
                    {
                        case DistributorTypeEnum.Direct:
                            result = new DefaultMessageDistributorDirect(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable, isDefault);
                            break;
                        case DistributorTypeEnum.Search:
                            switch (distributorSearchSourceType)
                            {
                                case DistributorSearchSourceTypeEnum.None:
                                    throw new System.Exception("none");
                                case DistributorSearchSourceTypeEnum.QueueKey:
                                    result = new DefaultMessageDistributorSearchQueueKey(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable, isDefault);
                                    break;
                                case DistributorSearchSourceTypeEnum.QueueTag:
                                    result = new DefaultMessageDistributorSearchQueueTag(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable, isDefault);
                                    break;
                                default:
                                    throw new System.Exception("none");
                            }
                            break;
                        default:
                            throw new System.Exception("none");
                    }
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
