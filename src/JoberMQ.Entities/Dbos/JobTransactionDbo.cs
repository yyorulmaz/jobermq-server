using JoberMQ.Entities.Base.Dbo;
using System;
using System.Collections.Generic;

namespace JoberMQ.Entities.Dbos
{
    internal class JobTransactionDbo : DboPropertyGuidBase, IDboBase
    {
        #region 5 - TIMING
        public bool IsTrigger { get; set; }
        public bool ErrorWorkflowStop { get; set; }
        public Guid? TriggerJobId { get; set; }
        public bool IsTriggerMain { get; set; }
        public Guid? TriggerGroupsId { get; set; }
        #endregion

        #region 6 - RESULT
        public bool IsResultMessageClientSend { get; set; }
        #endregion

        #region 7 - STATUS
        public bool IsCompleted { get; set; }
        public bool IsError { get; set; }
        #endregion

        #region 8 - CLONE CREATED
        public int Version { get; set; }
        public Guid CreatedJobId { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        public ICollection<JobTransactionDetailDbo> Details { get; set; }
        #endregion
    }
}
