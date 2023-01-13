using JoberMQ.Common.Dbos;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DboCreator;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Database.Data;
using JoberMQ.Database.Factories;
using JoberMQ.Library.Database.Factories;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TimerFramework;

namespace JoberMQ.Database.Implementation.DbService.Default
{
    internal class DfDatabase : IDatabase
    {
        public DfDatabase(IConfigurationDatabase configuration)
        {
            this.configuration=configuration;
            this.user = OprFactory.Create<UserDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, UserMemData.UserDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
            this.distributor = OprFactory.Create<DistributorDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, DistributorMemData.DistributorDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
            this.queue = OprFactory.Create<QueueDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, QueueMemData.QueueDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
            this.eventSub = OprFactory.Create<EventSubDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, EventSubMemData.EventSubDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "EventSub").Value);
            this.job = OprFactory.Create<JobDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, JobMemData.JobDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
            this.jobTransaction = OprFactory.Create<JobTransactionDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, JobTransactionMemData.JobTransactionDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
            this.message = OprFactory.Create<MessageDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, MessageMemData.MessageDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
            this.messageResult = OprFactory.Create<MessageResultDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, MessageResultMemData.MessageResultDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);
            
            
            this.dboCreator = DboCreatorFactory.CreateDboCreator(configuration.DboCreatorFactory, job);

            Setups();
        }


        IConfigurationDatabase configuration;


        private readonly IDboCreator dboCreator;
        public IDboCreator DboCreator => dboCreator;


        private readonly IOprRepositoryGuid<UserDbo> user;
        public IOprRepositoryGuid<UserDbo> User => user;


        private readonly IOprRepositoryGuid<DistributorDbo> distributor;
        public IOprRepositoryGuid<DistributorDbo> Distributor => distributor;


        private readonly IOprRepositoryGuid<QueueDbo> queue;
        public IOprRepositoryGuid<QueueDbo> Queue => queue;


        private readonly IOprRepositoryGuid<EventSubDbo> eventSub;
        public IOprRepositoryGuid<EventSubDbo> EventSub => eventSub;


        private readonly IOprRepositoryGuid<JobDbo> job;
        public IOprRepositoryGuid<JobDbo> Job => job;


        private readonly IOprRepositoryGuid<JobTransactionDbo> jobTransaction;
        public IOprRepositoryGuid<JobTransactionDbo> JobTransaction => jobTransaction;


        private readonly IOprRepositoryGuid<MessageDbo> message;
        public IOprRepositoryGuid<MessageDbo> Message => message;


        private readonly IOprRepositoryGuid<MessageResultDbo> messageResult;
        public IOprRepositoryGuid<MessageResultDbo> MessageResult => messageResult;


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
            // todo patlıyor kontrol
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

        public bool Setups()
        {
            User.Setups();
            Distributor.Setups();
            Queue.Setups();
            EventSub.Setups();
            Job.Setups();
            JobTransaction.Setups();
            Message.Setups();
            MessageResult.Setups();

            CompletedDataRemovesTimerStart(configuration.CompletedDataRemovesTimer);

            return true;
        }

        bool isRuningCompletedDataRemove = false;
        public bool IsRuningCompletedDataRemove => isRuningCompletedDataRemove;
    }
}
