using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Response;

namespace JoberMQ.Implementation.Distributor.Default
{
    internal class DefaultMessageDistributorDirect : MessageDistributorBase
    {
        public DefaultMessageDistributorDirect(string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault) : base(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable, isDefault)
        {
        }

        public override async Task<ResponseModel> Distributoring(MessageDbo message)
        {
            var result = new ResponseModel();

            var queue = JoberHost.JoberMQ.Queues.Get(message.Message.Routing.RoutingKey);
            if (queue != null)
                result = await queue.Queueing(message);
            else
            {
                result.IsOnline = true;
                result.IsSucces = false;
                result.Message = message.Message.Routing.RoutingKey + "  " + JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.6.1");
                result.IsQueue = false;
            }

            return result;
        }
    }
}
