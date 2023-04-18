﻿using JoberMQ.Configuration.Abstraction;
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
    internal class DfMessageDistributorFilter : MessageDistributorBase
    {
        public DfMessageDistributorFilter(IConfiguration configuration, IStatusCode statusCode, string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues, IDatabase database) : base(configuration, statusCode, distributorKey, distributorType, permissionType, isDurable, queues, database)
        {
        }

        public override async Task<ResponseModel> Distributoring(MessageDbo message)
        {
            var result = new ResponseModel();

            try
            {
                Regex regex = new Regex(message.Message.Routing.FilterRegex);
                var queueList = queues.MasterData.Where(x => regex.IsMatch(x.Value.QueueKey)).ToList();

                var filterGroupsId = Guid.NewGuid();
                if (queueList != null && queueList.Count > 0)
                {
                    foreach (var item in queueList)
                    {
                        message.Id = Guid.NewGuid();
                        message.FilterGroupsId = filterGroupsId;
                        var rslt = await queues.Get(item.Value.QueueKey).Queueing(message);
                        //todo hata kontrol
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
