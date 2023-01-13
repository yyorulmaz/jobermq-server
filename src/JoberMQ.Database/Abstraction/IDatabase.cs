using JoberMQ.Common.Dbos;
using JoberMQ.Database.Abstraction.DboCreator;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;

namespace JoberMQ.Database.Abstraction.DbService
{
    internal interface IDatabase
    {
        public IDboCreator DboCreator { get; }

        public IOprRepositoryGuid<UserDbo> User { get; }
        public IOprRepositoryGuid<DistributorDbo> Distributor { get; }
        public IOprRepositoryGuid<QueueDbo> Queue { get; }
        public IOprRepositoryGuid<EventSubDbo> EventSub { get; }
        public IOprRepositoryGuid<JobDbo> Job { get; }
        public IOprRepositoryGuid<JobTransactionDbo> JobTransaction { get; }
        public IOprRepositoryGuid<MessageDbo> Message { get; }
        public IOprRepositoryGuid<MessageResultDbo> MessageResult { get; }


        //todo aşağıdakilerden setups hariç diğerleri burada olmalımı

        //public bool ImportTextDataToSetMemDb();
        //public bool CreateDatabases();
        public bool Setups();
        //public bool DataGroupingAndSizes();

        bool CompletedDataRemovesTimerStart(string completedDataRemovesTimer);
        bool IsRuningCompletedDataRemove { get; }
    }
}
