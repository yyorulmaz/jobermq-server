using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Abstraction.DbOpr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TimerFramework;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfDbOprManager : IDbOprService
    {
        private readonly IUserDbOpr user;
        private readonly IDistributorDbOpr distributor;
        private readonly IQueueDbOpr queue;
        private readonly IEventSubDbOpr eventSub;
        private readonly IJobDbOpr job;
        private readonly IJobTransactionDbOpr jobTransaction;
        private readonly IMessageDbOpr message;
        private readonly IMessageResultDbOpr messageResult;

        public DfDbOprManager(
            IUserDbOpr user,
            IDistributorDbOpr distributor,
            IQueueDbOpr queue,
            IEventSubDbOpr eventSub,
            IJobDbOpr job,
            IJobTransactionDbOpr jobTransaction,
            IMessageDbOpr message,
            IMessageResultDbOpr messageResult)
        {
            this.user = user;
            this.distributor = distributor;
            this.queue = queue;
            this.eventSub = eventSub;
            this.job = job;
            this.jobTransaction = jobTransaction;
            this.message = message;
            this.messageResult = messageResult;

            
        }

        public IUserDbOpr User => user;
        public IDistributorDbOpr Distributor => distributor;
        public IQueueDbOpr Queue => queue;
        public IEventSubDbOpr EventSub => eventSub;
        public IJobDbOpr Job => job;
        public IJobTransactionDbOpr JobTransaction => jobTransaction;
        public IMessageDbOpr Message => message;
        public IMessageResultDbOpr MessageResult => messageResult;

        private ITimer completedDataRemoveTimer;
        public bool CompletedDataRemovesTimerStart(string completedDataRemovesTimer)
        {
            completedDataRemoveTimer = new TimerFactory().CreateTimer();
            completedDataRemoveTimer.Receive += CompletedDataRemoves;
            var timer = new TimerModel();
            timer.Id = Guid.NewGuid();
            //toco crontime check
            timer.CronTime = completedDataRemovesTimer;
            timer.TimerGroup = "CompletedDataRemoves";
            completedDataRemoveTimer.Add(timer);

            return true;
        }
        private void CompletedDataRemoves(TimerModel timer)
        {
            // TODO BURAYI KONTROL ET. tamamlanmış jobları silmeliyim
            // veya DataStatusTypeEnum u Delete olan jobları silmeliyim
            // bu durumumları yaparken ilişkili tablolarıda unutma, yani Job silinmişse veya Job tamamlanmış ise buna balı Job ve Message tablolarıda var
            if (isRuningCompletedDataRemove)
                return;

            isRuningCompletedDataRemove = true;

            var newJobs = new List<JobDbo>();
            var newJobTransactions = new List<JobTransactionDbo>();
            var newMessages = new List<MessageDbo>();

            var jobsAndPaths = job.DbText.ReadAllDataGrouping2(false);
            var jobTransactionsAndPaths = jobTransaction.DbText.ReadAllDataGrouping2(false);
            var messagesAndPaths = message.DbText.ReadAllDataGrouping2(false);

            var completedJobIdList = jobsAndPaths.datas.Where(x => x.IsCompleted == true).Select(s => s.Id).ToList();

            newJobs = jobsAndPaths.datas.Where(x => x.IsCompleted == false).ToList();
            newJobTransactions = jobTransactionsAndPaths.datas.Where(x => !completedJobIdList.Contains(x.CreatedJobId)).ToList();
            newMessages = messagesAndPaths.datas.Where(x => !completedJobIdList.Contains(x.CreatedJobId.Value)).ToList();

            #region Job
            var tempFileJob = job.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileJob);
            using (FileStream fs = job.DbText.FileStreamCreate(tempFileJob, 32768))
            {
                using (StreamWriter sw = job.DbText.StreamWriterCreate(fs))
                {
                    foreach (var item in newJobs)
                        sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }

            foreach (var item in jobsAndPaths.paths)
                File.Delete(item.FullPath);

            var arsiveFileJob = job.DbText.GetArsiveFileFullPath(1);

            if (job.DbText.ArsiveFileCounter == 1)
                job.DbText.ArsiveFileCounter = 2;

            File.Move(tempFileJob, arsiveFileJob);
            #endregion

            #region Job
            var tempFileJobTransaction = jobTransaction.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileJobTransaction);
            using (FileStream fs = jobTransaction.DbText.FileStreamCreate(tempFileJobTransaction, 32768))
            {
                using (StreamWriter sw = jobTransaction.DbText.StreamWriterCreate(fs))
                {
                    foreach (var item in newJobTransactions)
                        sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }

            foreach (var item in jobTransactionsAndPaths.paths)
                File.Delete(item.FullPath);

            var arsiveFileJobTransaction = jobTransaction.DbText.GetArsiveFileFullPath(1);

            if (jobTransaction.DbText.ArsiveFileCounter == 1)
                jobTransaction.DbText.ArsiveFileCounter = 2;

            File.Move(tempFileJobTransaction, arsiveFileJob);
            #endregion

            #region Message
            var tempFileMessage = message.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileMessage);
            using (FileStream fs = message.DbText.FileStreamCreate(tempFileMessage, 32768))
            {
                using (StreamWriter sw = message.DbText.StreamWriterCreate(fs))
                {
                    foreach (var item in newMessages)
                        sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }

            foreach (var item in messagesAndPaths.paths)
                File.Delete(item.FullPath);

            var arsiveFileMessage = message.DbText.GetArsiveFileFullPath(1);

            if (message.DbText.ArsiveFileCounter == 1)
                message.DbText.ArsiveFileCounter = 2;

            File.Move(tempFileMessage, arsiveFileMessage);
            #endregion

            isRuningCompletedDataRemove = false;
        }

        public bool ImportTextDataToSetMemDb()
        {
            var resultUser = User.ImportTextDataToSetMemDb();
            var resultDistributor = Distributor.ImportTextDataToSetMemDb();
            var resultQueue = Queue.ImportTextDataToSetMemDb();
            var resultEventSub = EventSub.ImportTextDataToSetMemDb();
            var resultJob = Job.ImportTextDataToSetMemDb();
            var resultJobTransaction = JobTransaction.ImportTextDataToSetMemDb();
            var resultMessage = Message.ImportTextDataToSetMemDb();
            var resultMessageResult = MessageResult.ImportTextDataToSetMemDb();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJob || !resultJobTransaction || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }

        public bool CreateDatabases()
        {
            var resultUser = User.CreateDatabase();
            var resultDistributor = Distributor.CreateDatabase();
            var resultQueue = Queue.CreateDatabase();
            var resultEventSub = EventSub.CreateDatabase();
            var resultJob = Job.CreateDatabase();
            var resultJobTransaction = JobTransaction.CreateDatabase();
            var resultMessage = Message.CreateDatabase();
            var resultMessageResult = MessageResult.CreateDatabase();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJob || !resultJobTransaction || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }
        public bool Setups()
        {
            var resultUser = User.Setup();
            var resultDistributor = Distributor.Setup();
            var resultQueue = Queue.Setup();
            var resultEventSub = EventSub.Setup();
            var resultJob= Job.Setup();
            var resultJobTransaction = JobTransaction.Setup();
            var resultMessage = Message.Setup();
            var resultMessageResult = MessageResult.Setup();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJob || !resultJobTransaction || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }
        public bool DataGroupingAndSizes()
        {
            var resultUser = User.DataGroupingAndSize();
            var resultDistributor = Distributor.DataGroupingAndSize();
            var resultQueue = Queue.DataGroupingAndSize();
            var resultEventSub = EventSub.DataGroupingAndSize();
            var resultJob = Job.DataGroupingAndSize();
            var resultJobTransaction = JobTransaction.DataGroupingAndSize();
            var resultMessage = Message.DataGroupingAndSize();
            var resultMessageResult = MessageResult.DataGroupingAndSize();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJob || !resultJobTransaction || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }

        bool isRuningCompletedDataRemove = false;
        public bool IsRuningCompletedDataRemove => isRuningCompletedDataRemove;
        
    }
}
