using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.DboCreator
{
    internal interface IDboCreator
    {
        public JobTransactionDbo JobTransactionDboCreate(JobDbo jobDbo);
        public MessageDbo MessageDboCreate(JobDbo jobDbo, JobDetailDbo jobDetailDbo, JobTransactionDbo jobTransactionDbo, JobTransactionDetailDbo jobTransactionDetailDbo, Guid? eventGroupId);
        public List<MessageDbo> MessageDboCreates(JobTransactionDbo jobDbo);
        public List<JobTransactionDbo> CloneJobToJobTransactions(JobDbo job);

    }
}
