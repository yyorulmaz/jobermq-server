﻿using JoberMQ.Common.Database.Factories;
using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TimerFramework;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Database;
using JoberMQ.Data;
using JoberMQ.Factories.Database;

namespace JoberMQ.Implementation.Database.Default
{
    internal class DefaultDatabase : IDatabase
    {
        public DefaultDatabase(IConfigurationDatabase configuration)
        {
            this.configuration = configuration;
            user = OprFactory.Create<UserDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, UserMemData.UserDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "User").Value);
            distributor = OprFactory.Create<DistributorDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, DistributorMemData.DistributorDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Distributor").Value);
            queue = OprFactory.Create<QueueDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, QueueMemData.QueueDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Queue").Value);
            subscript = OprFactory.Create<SubscriptDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, SubscriptMemData.SubscriptDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Subscript").Value);
            job = OprFactory.Create<JobDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, JobMemData.JobDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Job").Value);
            jobTransaction = OprFactory.Create<JobTransactionDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, JobTransactionMemData.JobTransactionDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "JobTransaction").Value);
            message = OprFactory.Create<MessageDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, MessageMemData.MessageDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "Message").Value);
            messageResult = OprFactory.Create<MessageResultDbo>(configuration.DbOprFactory, configuration.DbMemFactory, configuration.DbMemDataFactory, MessageResultMemData.MessageResultDatas, configuration.DbTextFactory, configuration.DbTextFileConfigDatas.FirstOrDefault(x => x.Key == "MessageResult").Value);


            dboCreator = DboCreatorFactory.CreateDboCreator(configuration.DboCreatorFactory, job);

            //Setups();
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


        private readonly IOprRepositoryGuid<SubscriptDbo> subscript;
        public IOprRepositoryGuid<SubscriptDbo> Subscript => subscript;


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

            // todo hata veriyordu düzelt 116.satırda
            return;

            isRuningCompletedDataRemove = true;

            var newJobs = new List<JobDbo>();
            var newJobTransactions = new List<JobTransactionDbo>();
            var newMessages = new List<MessageDbo>();

            var jobsAndPaths = job.DbText.ReadAllDataGrouping2(false);
            var jobTransactionsAndPaths = jobTransaction.DbText.ReadAllDataGrouping2(false);
            var messagesAndPaths = message.DbText.ReadAllDataGrouping2(false);

            var completedJobIdList = jobsAndPaths.datas.Where(x => x.Status.IsCompleted == true).Select(s => s.Id).ToList();

            newJobs = jobsAndPaths.datas.Where(x => x.Status.IsCompleted == false).ToList();
            newJobTransactions = jobTransactionsAndPaths.datas.Where(x => !completedJobIdList.Contains(x.CreatedJobId)).ToList();
            newMessages = messagesAndPaths.datas.Where(x => !completedJobIdList.Contains(x.CreatedJobId.Value)).ToList();

            #region Job
            var tempFileJob = job.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileJob).Close();
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
            File.Create(tempFileJobTransaction).Close();
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

            //todo burada hata veriyor kontrol et
            File.Move(tempFileJobTransaction, arsiveFileJobTransaction);
            #endregion

            #region Message
            var tempFileMessage = message.DbText.GetArsiveFileFullPath(0);
            File.Create(tempFileMessage).Close();
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
            Subscript.Setups();
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