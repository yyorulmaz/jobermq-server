using JoberMQ.Database.Abstraction.DbOpr;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Abstraction.Database
{
    internal interface IDatabaseService
    {
        public IUserDbOpr User { get; }
        public IDistributorDbOpr Distributor { get; }
        public IQueueDbOpr Queue { get; }
        public IEventSubDbOpr EventSub { get; }
        public IJobDbOpr Job { get; }
        public IJobTransactionDbOpr JobTransaction { get; }
        public IMessageDbOpr Message { get; }
        public IMessageResultDbOpr MessageResult { get; }

        public bool ImportTextDataToSetMemDb();
        public bool CreateDatabases();
        public bool Setups();
        public bool DataGroupingAndSizes();

        bool CompletedDataRemovesTimerStart(string completedDataRemovesTimer);
        bool IsRuningCompletedDataRemove { get; }
    }
}
