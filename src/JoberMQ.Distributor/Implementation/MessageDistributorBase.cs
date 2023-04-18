using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Distributor.Abstraction;
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
using System.Threading.Tasks;

namespace JoberMQ.Distributor.Implementation
{
    internal abstract class MessageDistributorBase : IMessageDistributor
    {
        private readonly string distributorKey;
        protected readonly IMemRepository<string, IMessageQueue> queues;
        protected readonly IConfiguration configuration;
        protected readonly IStatusCode statusCode;
        protected readonly IDatabase database;

        public MessageDistributorBase(IConfiguration configuration, IStatusCode statusCode, string distributorKey, DistributorTypeEnum distributorType, PermissionTypeEnum permissionType, bool isDurable, IMemRepository<string, IMessageQueue> queues, IDatabase database)
        {
            this.configuration = configuration;
            this.statusCode = statusCode;
            this.distributorKey = distributorKey;
            this.distributorType = distributorType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.queues = queues;
            this.database = database;
        }

        public string DistributorKey => distributorKey;

        private DistributorTypeEnum distributorType;
        public DistributorTypeEnum DistributorType { get => distributorType; set => distributorType = value; }
        private PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType { get => permissionType; set => permissionType = value; }
        private bool isDurable;
        public bool IsDurable { get => isDurable; set => isDurable = value; }


        public abstract Task<ResponseModel> Distributoring(MessageDbo message);
    }
}
