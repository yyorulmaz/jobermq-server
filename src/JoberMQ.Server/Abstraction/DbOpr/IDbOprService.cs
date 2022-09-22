﻿namespace JoberMQ.Server.Abstraction.DbOpr
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
        public bool Setup();
        public bool DataGroupingAndSize();
    }
}
