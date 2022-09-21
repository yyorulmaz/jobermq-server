using JoberMQ.Server.Abstraction.DbOpr;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfDbOprManager : IDbOprService
    {
        private readonly IUserDbOpr user;
        private readonly IDistributorDbOpr distributor;
        private readonly IQueueDbOpr queue;
        private readonly IEventSubDbOpr eventSub;
        private readonly IJobDataDbOpr jobData;
        private readonly IJobDbOpr job;
        private readonly IMessageDbOpr message;
        private readonly IMessageResultDbOpr messageResult;

        public DfDbOprManager(
            IUserDbOpr user,
            IDistributorDbOpr distributor,
            IQueueDbOpr queue,
            IEventSubDbOpr eventSub,
            IJobDataDbOpr jobData,
            IJobDbOpr job,
            IMessageDbOpr message,
            IMessageResultDbOpr messageResult)
        {
            this.user = user;
            this.distributor = distributor;
            this.queue = queue;
            this.eventSub = eventSub;
            this.jobData = jobData;
            this.job = job;
            this.message = message;
            this.messageResult = messageResult;
        }

        public IUserDbOpr User => user;
        public IDistributorDbOpr Distributor => distributor;
        public IQueueDbOpr Queue => queue;
        public IEventSubDbOpr EventSub => eventSub;
        public IJobDataDbOpr JobData => jobData;
        public IJobDbOpr Job => job;
        public IMessageDbOpr Message => message;
        public IMessageResultDbOpr MessageResult => messageResult;

        public bool ImportTextDataToSetMemDb()
        {
            var resultUser = User.ImportTextDataToSetMemDb();
            var resultDistributor = Distributor.ImportTextDataToSetMemDb();
            var resultQueue = Queue.ImportTextDataToSetMemDb();
            var resultEventSub = EventSub.ImportTextDataToSetMemDb();
            var resultJobData = JobData.ImportTextDataToSetMemDb();
            var resultJob = Job.ImportTextDataToSetMemDb();
            var resultMessage = Message.ImportTextDataToSetMemDb();
            var resultMessageResult = MessageResult.ImportTextDataToSetMemDb();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJobData || !resultJob || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }

        public bool TextDataSetup()
        {
            var resultUser = User.TextDataSetup();
            var resultDistributor = Distributor.TextDataSetup();
            var resultQueue = Queue.TextDataSetup();
            var resultEventSub = EventSub.TextDataSetup();
            var resultJobData = JobData.TextDataSetup();
            var resultJob = Job.TextDataSetup();
            var resultMessage = Message.TextDataSetup();
            var resultMessageResult = MessageResult.TextDataSetup();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJobData || !resultJob || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }
    }
}
