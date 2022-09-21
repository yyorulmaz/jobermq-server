using JoberMQ.Entities.Base.Dbo;
using System;

namespace JoberMQ.Entities.Dbos
{
    internal class JobDetailDbo : DboPropertyGuidBase, IDboBase
    {
        #region 3 - MESSAGE

        #region 3.4 - RESULT ROUTING 
        public bool IsResultMessageClientSend { get; set; }
        #endregion

        #endregion

        #region 8 - CLONE CREATED
        public Guid? CreatedJobDataDetailId { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        //public JobDbo JobDbo { get; set; }
        public Guid? JobId { get; set; }
        #endregion
    }
}
