using JoberMQ.Common.Models;
using JoberMQ.Library.Database.Base;
using System;
using System.Collections.Generic;

namespace JoberMQ.Common.Dbos
{
    public class JobTransactionDbo : DboPropertyGuidBase, IDboBase
    {
        public TimingModel Timing { get; set; }
        public StatusModel Status { get; set; }


        public bool IsResultMessageClientSend { get; set; }
        public int Version { get; set; }
        public Guid CreatedJobId { get; set; }

        public List<JobTransactionDetailDbo> JobTransactioDetails { get; set; }

    }
}
