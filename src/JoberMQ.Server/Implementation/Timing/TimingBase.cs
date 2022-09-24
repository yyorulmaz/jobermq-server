using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;

namespace JoberMQ.Server.Implementation.Timing
{
    internal abstract class TimingBase : ITiming
    {
        protected readonly IDbOprService dbOprService;
        protected readonly IDboCreator dboCreator;
        protected readonly ISchedule schedule;
        public TimingBase(IDbOprService dbOprService)
        {
            this.dbOprService = dbOprService;
        }
        public TimingBase(IDbOprService dbOprService, IDboCreator dboCreator)
        {
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
        }
        public TimingBase(IDbOprService dbOprService, ISchedule schedule)
        {
            this.dbOprService = dbOprService;
            this.schedule = schedule;
        }
        public TimingBase(IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
            this.schedule = schedule;
        }


        public abstract JobDataAddResponseModel Timing(JobDataDbo jobData);
    }
}
