using JoberMQ.Entities.Enums.DbOpr;
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
            IJobDataDbOpr JobDataDbOpr,
            IJobDbOpr JobDbOpr,
            IMessageDbOpr MessageDbOpr,
            IMessageResultDbOpr MessageResultDbOpr)
            CreateDbOprs(DbOprFactoryEnum dbOprFactory, DbMemFactoryEnum dbMemFactory, DbMemDataFactoryEnum dbMemDataFactory, DbTextFactoryEnum dbTextFactory)
        {
            var dbMems = DbMemFactory.CreateDbMems(dbMemFactory, dbMemDataFactory);
            var dbTexts = DbTextFactory.CreateDbTexts(dbTextFactory);

            IUserDbOpr userDbOpr;
            IDistributorDbOpr distributorDbOpr;
            IQueueDbOpr queueDbOpr;
            IEventSubDbOpr eventSubDbOpr;
            IJobDataDbOpr jobDataDbOpr;
            IJobDbOpr jobDbOpr;
            IMessageDbOpr messageDbOpr;
            IMessageResultDbOpr messageResultDbOpr;

            switch (dbOprFactory)
            {
                case DbOprFactoryEnum.Default:
                    userDbOpr = new DfUserDbOpr(dbMems.UserMemDal, dbTexts.UserTextDal);
                    distributorDbOpr = new DfDistributorDbOpr(dbMems.DistributorMemDal, dbTexts.DistributorTextDal);
                    queueDbOpr = new DfQueueDbOpr(dbMems.QueueMemDal, dbTexts.QueueTextDal);
                    eventSubDbOpr = new DfEventSubDbOpr(dbMems.EventSubMemDal, dbTexts.EventSubTextDal);
                    jobDataDbOpr = new DfJobDataDbOpr(dbMems.JobDataMemDal, dbTexts.JobDataTextDal);
                    jobDbOpr = new DfJobDbOpr(dbMems.JobMemDal, dbTexts.JobTextDal);
                    messageDbOpr = new DfMessageDbOpr(dbMems.MessageMemDal, dbTexts.MessageTextDal);
                    messageResultDbOpr = new DfMessageResultDbOpr(dbMems.MessageResultMemDal, dbTexts.MessageResultTextDal);
                    break;
                default:
                    userDbOpr = new DfUserDbOpr(dbMems.UserMemDal, dbTexts.UserTextDal);
                    distributorDbOpr = new DfDistributorDbOpr(dbMems.DistributorMemDal, dbTexts.DistributorTextDal);
                    queueDbOpr = new DfQueueDbOpr(dbMems.QueueMemDal, dbTexts.QueueTextDal);
                    eventSubDbOpr = new DfEventSubDbOpr(dbMems.EventSubMemDal, dbTexts.EventSubTextDal);
                    jobDataDbOpr = new DfJobDataDbOpr(dbMems.JobDataMemDal, dbTexts.JobDataTextDal);
                    jobDbOpr = new DfJobDbOpr(dbMems.JobMemDal, dbTexts.JobTextDal);
                    messageDbOpr = new DfMessageDbOpr(dbMems.MessageMemDal, dbTexts.MessageTextDal);
                    messageResultDbOpr = new DfMessageResultDbOpr(dbMems.MessageResultMemDal, dbTexts.MessageResultTextDal);
                    break;
            }

            return (userDbOpr, distributorDbOpr, queueDbOpr, eventSubDbOpr, jobDataDbOpr, jobDbOpr, messageDbOpr, messageResultDbOpr);
        }




    }
}
