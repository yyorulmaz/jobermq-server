using JoberMQ.Entities.Enums.Data;
using JoberMQ.Entities.Helper;
using System;

namespace JoberMQ.Entities.Base.Dbo
{
    public class DboPropertyGuidBase
    {
        public DboPropertyGuidBase()
        {
            IsActive = true;
            IsDelete = false;
            CreateDate = DateHelper.GetUniversalNow();
            ProcessTime = DateHelper.GetUniversalNow();
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }


        public Guid TransactionId { get; set; }
        public bool IsTransactionCompleted { get; set; } = false;
        public DateTime TransactionDate { get; set; }


        public DateTime ProcessTime { get; set; }
        public DataStatusTypeEnum DataStatusType { get; set; }
    }
}
