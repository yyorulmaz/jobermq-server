using System.Threading.Tasks;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Response;
using JoberMQ.Abstraction.Distributor;

namespace JoberMQ.Implementation.Distributor
{
    internal abstract class MessageDistributorBase : IMessageDistributor
    {
        protected MessageDistributorBase(string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable, bool isDefault)
        {
            this.distributorKey=distributorKey;
            this.distributorType=distributorType;
            this.distributorSearchSourceType=distributorSearchSourceType;
            this.permissionType=permissionType;
            this.isDurable=isDurable;
            this.isDefault=isDefault;
        }

        string distributorKey;
        public string DistributorKey { get => distributorKey; set => distributorKey = value; }


        readonly DistributorTypeEnum distributorType;
        public DistributorTypeEnum DistributorType => distributorType;


        readonly DistributorSearchSourceTypeEnum distributorSearchSourceType;
        public DistributorSearchSourceTypeEnum DistributorSearchSourceType => distributorSearchSourceType;


        PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType { get => permissionType; set => permissionType = value; }


        bool isDurable;
        public bool IsDurable { get => isDurable; set => isDurable = value; }


        readonly bool isDefault;
        public bool IsDefault => isDefault;


        public abstract Task<ResponseModel> Distributoring(MessageDbo message);
    }
}
