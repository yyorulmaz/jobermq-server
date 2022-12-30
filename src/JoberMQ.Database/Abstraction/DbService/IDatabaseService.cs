using JoberMQ.Database.Abstraction.DBOCreator;
using JoberMQ.Database.Abstraction.DbOpr;

namespace JoberMQ.Database.Abstraction.DbService
{
    internal interface IDatabaseService
    {
        public IDboCreator DboCreator { get; }

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
