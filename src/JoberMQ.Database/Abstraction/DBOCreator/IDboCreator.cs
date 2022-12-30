using JoberMQ.Common.Dbos;
using System;
using System.Collections.Generic;

namespace JoberMQ.Database.Abstraction.DBOCreator
{
    internal interface IDboCreator
    {
        public JobTransactionDbo JobTransactionDboCreate(JobDbo jobDbo);
        public MessageDbo MessageDboCreate(JobDbo jobDbo, JobDetailDbo jobDetailDbo, JobTransactionDbo jobTransactionDbo, JobTransactionDetailDbo jobTransactionDetailDbo, Guid? eventGroupId);
        public List<MessageDbo> MessageDboCreates(JobTransactionDbo jobDbo);
        public List<JobTransactionDbo> CloneJobToJobTransactions(JobDbo job);

    }
}
