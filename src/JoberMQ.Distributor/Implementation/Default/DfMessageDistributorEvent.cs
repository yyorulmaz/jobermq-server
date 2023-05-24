using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Queue.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Implementation.Default
{
    internal class DfMessageDistributorEvent : MessageDistributorBase
    {
        public DfMessageDistributorEvent(IConfiguration configuration, IStatusCode statusCode, string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues, IDatabase database) : base(configuration, statusCode, distributorKey, distributorType, permissionType, isDurable, queues, database)
        {
        }

        public override async Task<ResponseModel> Distributoring(MessageDbo message)
        {
            var result = new ResponseModel();

            try
            {
                var subList = database.Subscript.GetAll(x => x.IsActive == true && x.IsDelete == false && x.EventKey == message.Message.Routing.EventName).ToList();

                var eventGroupsId = Guid.NewGuid();
                if (subList != null && subList.Count > 0)
                {
                    foreach (var item in subList)
                    {
                        message.Id = Guid.NewGuid();
                        message.EventGroupsId = eventGroupsId;

                        message.Message.Routing.ClientKey = item.ClientKey;

                        //var rslt = await queues.Get(item.EventKey).Queueing(message);
                        var que = queues.Get(item.EventKey);
                        if (que != null)
                            await que.Queueing(message);
                    }

                }
            }
            catch (Exception)
            {
                result.IsOnline = true;
                result.IsSucces = false;
                result.Message = ""; //todo hata mesajı
            }

            return result;
        }
    }
}
