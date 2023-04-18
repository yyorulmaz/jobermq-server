using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Database.Abstraction;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Routing;
using JoberMQ.Library.Models.Queue;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Queue.Abstraction;
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Implementation.Default
{
    internal class DfMessageDistributorDirect : MessageDistributorBase
    {
        public DfMessageDistributorDirect(IConfiguration configuration, IStatusCode statusCode, string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues, IDatabase database) : base(configuration, statusCode, distributorKey, distributorType, permissionType, isDurable, queues, database)
        {
        }

        public override async Task<ResponseModel> Distributoring(MessageDbo message)
        {
            var result = new ResponseModel();

            var queue = queues.Get(message.Message.Routing.QueueKey);
            if (queue != null)
                result = await queue.Queueing(message);
            else
            {
                result.IsOnline = true;
                result.IsSucces = false;
                result.Message = message.Message.Routing.QueueKey + "  " + statusCode.GetStatusMessage("1.6.1");
                result.IsQueue = false;
            }

            return result;
        }
    }
}
