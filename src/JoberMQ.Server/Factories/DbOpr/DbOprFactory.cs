using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Implementation.DbOpr.Default;

namespace JoberMQ.Server.Factories.DbOpr
{
    internal class DbOprFactory
    {
        internal static (
            IUserDbOpr UserDbOpr,
            IDistributorDbOpr DistributorDbOpr,
            IQueueDbOpr QueueDbOpr,
            IEventSubDbOpr EventSubDbOpr,
            IJobDbOpr JobDbOpr,
            IJobTransactionDbOpr JobTransactionDbOpr,
            IMessageDbOpr MessageDbOpr,
            IMessageResultDbOpr MessageResultDbOpr)
            //CreateDbOprs(DbOprFactoryEnum dbOprFactory, DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory, DbTextFactoryEnum dbTextFactory)
            CreateDbOprs(DbOprConfigModel dbOprConfig)
        {
            var dbMems = DbMemFactory.CreateDbMems(dbOprConfig.DbMemConfig.DbMemFactory, dbOprConfig.DbMemConfig.DbMemDataFactory);
            var dbTexts = DbTextFactory.CreateDbTexts(dbOprConfig.DbTextConfig);

            IUserDbOpr userDbOpr;
            IDistributorDbOpr distributorDbOpr;
            IQueueDbOpr queueDbOpr;
            IEventSubDbOpr eventSubDbOpr;
            IJobDbOpr jobDbOpr;
            IJobTransactionDbOpr jobTransactionDbOpr;
            IMessageDbOpr messageDbOpr;
            IMessageResultDbOpr messageResultDbOpr;

            switch (dbOprConfig.DbOprFactory)
            {
                case DbOprFactoryEnum.Default:
                    userDbOpr = new DfUserDbOpr(dbMems.UserMemDal, dbTexts.UserTextDal);
                    distributorDbOpr = new DfDistributorDbOpr(dbMems.DistributorMemDal, dbTexts.DistributorTextDal);
                    queueDbOpr = new DfQueueDbOpr(dbMems.QueueMemDal, dbTexts.QueueTextDal);
                    eventSubDbOpr = new DfEventSubDbOpr(dbMems.EventSubMemDal, dbTexts.EventSubTextDal);
                    jobDbOpr = new DfJobDbOpr(dbMems.JobMemDal, dbTexts.JobTextDal);
                    jobTransactionDbOpr = new DfJobTransactionDbOpr(dbMems.JobTransactionMemDal, dbTexts.JobTransactionTextDal);
                    messageDbOpr = new DfMessageDbOpr(dbMems.MessageMemDal, dbTexts.MessageTextDal);
                    messageResultDbOpr = new DfMessageResultDbOpr(dbMems.MessageResultMemDal, dbTexts.MessageResultTextDal);
                    break;
                default:
                    userDbOpr = new DfUserDbOpr(dbMems.UserMemDal, dbTexts.UserTextDal);
                    distributorDbOpr = new DfDistributorDbOpr(dbMems.DistributorMemDal, dbTexts.DistributorTextDal);
                    queueDbOpr = new DfQueueDbOpr(dbMems.QueueMemDal, dbTexts.QueueTextDal);
                    eventSubDbOpr = new DfEventSubDbOpr(dbMems.EventSubMemDal, dbTexts.EventSubTextDal);
                    jobDbOpr = new DfJobDbOpr(dbMems.JobMemDal, dbTexts.JobTextDal);
                    jobTransactionDbOpr = new DfJobTransactionDbOpr(dbMems.JobTransactionMemDal, dbTexts.JobTransactionTextDal);
                    messageDbOpr = new DfMessageDbOpr(dbMems.MessageMemDal, dbTexts.MessageTextDal);
                    messageResultDbOpr = new DfMessageResultDbOpr(dbMems.MessageResultMemDal, dbTexts.MessageResultTextDal);
                    break;
            }

            return (userDbOpr, distributorDbOpr, queueDbOpr, eventSubDbOpr, jobDbOpr, jobTransactionDbOpr, messageDbOpr, messageResultDbOpr);
        }




    }
}
