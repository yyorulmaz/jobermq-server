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
        private readonly IJobDataDbOpr jobData;
        private readonly IJobDbOpr job;
        private readonly IMessageDbOpr message;
        private readonly IMessageResultDbOpr messageResult;
        private readonly ITimer completedDataRemoveTimer;

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

            completedDataRemoveTimer = new TimerFactory().CreateTimer();
            completedDataRemoveTimer.Receive += CompletedDataRemoves;
            var timer = new TimerModel();
            timer.Id = Guid.NewGuid();
            timer.CronTime = "0 */5 * ? * *";
            timer.TimerGroup = "CompletedDataRemoves";
            completedDataRemoveTimer.Add(timer);
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

        public bool Setups()
        {
            var resultUser = User.Setup();
            var resultDistributor = Distributor.Setup();
            var resultQueue = Queue.Setup();
            var resultEventSub = EventSub.Setup();
            var resultJobData = JobData.Setup();
            var resultJob = Job.Setup();
            var resultMessage = Message.Setup();
            var resultMessageResult = MessageResult.Setup();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJobData || !resultJob || !resultMessage || !resultMessageResult)
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
            var resultJobData = JobData.DataGroupingAndSize();
            var resultJob = Job.DataGroupingAndSize();
            var resultMessage = Message.DataGroupingAndSize();
            var resultMessageResult = MessageResult.DataGroupingAndSize();

            if (!resultUser || !resultDistributor || !resultQueue || !resultEventSub || !resultJobData || !resultJob || !resultMessage || !resultMessageResult)
            {
                throw new System.Exception("errorrrr ");
            }

            return true;
        }

        bool isRuningCompletedDataRemove = false;
        public bool IsRuningCompletedDataRemove => isRuningCompletedDataRemove;
        public void CompletedDataRemoves(TimerModel timer)
        {
            isRuningCompletedDataRemove = true;

            var newJobDatas = new List<JobDataDbo>();
            var newJobs = new List<JobDbo>();
            var newMessages = new List<MessageDbo>();

            var jobDatasAndPaths = jobData.DbText.ReadAllDataGrouping2(false);
            var jobsAndPaths = job.DbText.ReadAllDataGrouping2(false);
            var messagesAndPaths = message.DbText.ReadAllDataGrouping2(false);

            var completedJobDataIdList = jobDatasAndPaths.datas.Where(x => x.IsCompleted == true).Select(s => s.Id).ToList();

            newJobDatas = jobDatasAndPaths.datas.Where(x => x.IsCompleted == false).ToList();
            newJobs = jobsAndPaths.datas.Where(x => !completedJobDataIdList.Contains(x.CreatedJobDataId)).ToList();
            newMessages = messagesAndPaths.datas.Where(x => !completedJobDataIdList.Contains(x.CreatedJobDataId.Value)).ToList();

            #region JobData
            var tempFileJobData = jobData.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileJobData);
            using (StreamWriter sw = new StreamWriter(tempFileJobData, true, Encoding.UTF8))
            {
                foreach (var item in newJobDatas)
                    sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            foreach (var item in jobDatasAndPaths.paths)
                File.Delete(item.FullPath);

            var arsiveFileJobData = jobData.DbText.GetArsiveFileFullPath(1);

            if (jobData.DbText.ArsiveFileCounter == 1)
                jobData.DbText.ArsiveFileCounter = 2;

            File.Move(tempFileJobData, arsiveFileJobData);
            #endregion

            #region Job
            var tempFileJob = job.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileJob);
            using (StreamWriter sw = new StreamWriter(tempFileJob, true, Encoding.UTF8))
            {
                foreach (var item in newJobs)
                    sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            foreach (var item in jobsAndPaths.paths)
                File.Delete(item.FullPath);

            var arsiveFileJob = job.DbText.GetArsiveFileFullPath(1);

            if (job.DbText.ArsiveFileCounter == 1)
                job.DbText.ArsiveFileCounter = 2;

            File.Move(tempFileJob, arsiveFileJob);
            #endregion

            #region Message
            var tempFileMessage = message.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileMessage);
            using (StreamWriter sw = new StreamWriter(tempFileMessage, true, Encoding.UTF8))
            {
                foreach (var item in newMessages)
                    sw.WriteLine(JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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
    }
}
