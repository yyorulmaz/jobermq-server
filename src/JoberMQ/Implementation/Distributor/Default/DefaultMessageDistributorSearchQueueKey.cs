using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Response;
using JoberMQ.Abstraction.Queue;

namespace JoberMQ.Implementation.Distributor.Default
{
    internal class DefaultMessageDistributorSearchQueueKey : MessageDistributorBase
    {
        public DefaultMessageDistributorSearchQueueKey(string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault) : base(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable, isDefault)
        {
        }

        public override async Task<ResponseModel> Distributoring(MessageDbo message)
        {
            var response = new ResponseModel();
            response.IsOnline = true;
            //response.Id = job.Id;

            List<IMessageQueue> matchingModels = new List<IMessageQueue>();

            foreach (IMessageQueue que in JoberHost.JoberMQ.Queues.GetAll())
            {
                if (Regex.IsMatch(que.QueueKey, message.Message.Routing.RoutingKey))
                {
                    matchingModels.Add(que);
                }
            }

            if (matchingModels == null || matchingModels.Count == 0)
            {
                response.IsSucces = false; 
                return response;
            }

            foreach (IMessageQueue que in matchingModels)
            {
                // todo burada mesajı işaretlemem gerek çünkü çoğaltmış oluyorum
                // bir counter da tutabilirim, her mesaja bir groupId gibi bir değer de atayabilirim

                // birsürü response dönecek neolacak nasıl kontrol edeceğim
                response = await JoberHost.JoberMQ.Queues.Get(que.QueueKey).Queueing(message);
            }

            return response;
        }
    }
}
