using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.DataAccess.Implementation.DbText.Default;
using JoberMQ.Entities.Enums.DbOpr;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DbTextFactory
    {
        internal static (
            IUserTextDal UserTextDal,
            IDistributorTextDal DistributorTextDal,
            IQueueTextDal QueueTextDal,
            IEventSubTextDal EventSubTextDal,
            IJobDataTextDal JobDataTextDal,
            IJobTextDal JobTextDal,
            IMessageTextDal MessageTextDal,
            IMessageResultTextDal MessageResultTextDal)
            CreateDbTexts(DbTextFactoryEnum dbTextFactory)
        {
            IUserTextDal userTextDal;
            IDistributorTextDal distributorTextDal;
            IQueueTextDal queueTextDal;
            IEventSubTextDal eventSubTextDal;
            IJobDataTextDal jobDataTextDal;
            IJobTextDal jobTextDal;
            IMessageTextDal messageTextDal;
            IMessageResultTextDal messageResultTextDal;

            switch (dbTextFactory)
            {
                case DbTextFactoryEnum.Default:
                    userTextDal = new DfUserTextDal("Database", "User", "User", '.', '_', "txt", 100000);
                    distributorTextDal = new DfDistributorTextDal("Database", "Distributor", "Distributor", '.', '_', "txt", 100000);
                    queueTextDal = new DfQueueTextDal("Database", "Queue", "Queue", '.', '_', "txt", 100000);
                    eventSubTextDal = new DfEventSubTextDal("Database", "EventSub", "EventSub", '.', '_', "txt", 100000);
                    jobDataTextDal = new DfJobDataTextDal("Database", "JobData", "JobData", '.', '_', "txt", 100000);
                    jobTextDal = new DfJobTextDal("Database", "Job", "Job", '.', '_', "txt", 100000);
                    messageTextDal = new DfMessageTextDal("Database", "Message", "Message", '.', '_', "txt", 100000);
                    messageResultTextDal = new DfMessageResultTextDal("Database", "MessageResult", "MessageResult", '.', '_', "txt", 100000);
                    break;
                default:
                    userTextDal = new DfUserTextDal("Database", "User", "User", '.', '_', "txt", 100000);
                    distributorTextDal = new DfDistributorTextDal("Database", "Distributor", "Distributor", '.', '_', "txt", 100000);
                    queueTextDal = new DfQueueTextDal("Database", "Queue", "Queue", '.', '_', "txt", 100000);
                    eventSubTextDal = new DfEventSubTextDal("Database", "EventSub", "EventSub", '.', '_', "txt", 100000);
                    jobDataTextDal = new DfJobDataTextDal("Database", "JobData", "JobData", '.', '_', "txt", 100000);
                    jobTextDal = new DfJobTextDal("Database", "Job", "Job", '.', '_', "txt", 100000);
                    messageTextDal = new DfMessageTextDal("Database", "Message", "Message", '.', '_', "txt", 100000);
                    messageResultTextDal = new DfMessageResultTextDal("Database", "MessageResult", "MessageResult", '.', '_', "txt", 100000);
                    break;
            }

            return (userTextDal, distributorTextDal, queueTextDal, eventSubTextDal, jobDataTextDal, jobTextDal, messageTextDal, messageResultTextDal);
        }
    }
}
