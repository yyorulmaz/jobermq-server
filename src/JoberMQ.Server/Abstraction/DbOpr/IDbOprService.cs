using TimerFramework;

namespace JoberMQ.Server.Abstraction.DbOpr
{
    internal interface IDbOprService
    {
        public IUserDbOpr User { get; }
        public IDistributorDbOpr Distributor { get; }
        public IQueueDbOpr Queue { get; }
        public IEventSubDbOpr EventSub { get; }
        public IJobDataDbOpr JobData { get; }
        public IJobDbOpr Job { get; }
        public IMessageDbOpr Message { get; }
        public IMessageResultDbOpr MessageResult { get; }

        public bool ImportTextDataToSetMemDb();
        public bool CreateDatabases();
        public bool Setups();
        public bool DataGroupingAndSizes();

        bool CompletedDataRemovesTimerStart();
        bool IsRuningCompletedDataRemove { get; }
    }
}
