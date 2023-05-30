﻿using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;

namespace JoberMQ.Abstraction.Database
{
    public interface IDatabase
    {
        public IDboCreator DboCreator { get; }

        public IOprRepositoryGuid<UserDbo> User { get; }
        public IOprRepositoryGuid<DistributorDbo> Distributor { get; }
        public IOprRepositoryGuid<QueueDbo> Queue { get; }
        public IOprRepositoryGuid<SubscriptDbo> Subscript { get; }
        public IOprRepositoryGuid<JobDbo> Job { get; }
        public IOprRepositoryGuid<JobTransactionDbo> JobTransaction { get; }
        public IOprRepositoryGuid<MessageDbo> Message { get; }
        public IOprRepositoryGuid<MessageResultDbo> MessageResult { get; }

        public bool Setups();

        bool CompletedDataRemovesTimerStart(string completedDataRemovesTimer);
        bool IsRuningCompletedDataRemove { get; }
    }
}