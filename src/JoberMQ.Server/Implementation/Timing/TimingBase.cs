using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;

namespace JoberMQ.Server.Implementation.Timing
{
    internal abstract class TimingBase : ITiming
    {
        protected readonly IBroker broker;
        protected readonly IDbOprService dbOprService;
        protected readonly IDboCreator dboCreator;
        protected readonly ISchedule schedule;
        public TimingBase(IBroker broker, IDbOprService dbOprService)
        {
            this.broker = broker;
            this.dbOprService = dbOprService;
        }
        public TimingBase(IBroker broker, IDbOprService dbOprService, IDboCreator dboCreator)
        {
            this.broker = broker;
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
        }
        public TimingBase(IBroker broker, IDbOprService dbOprService, ISchedule schedule)
        {
            this.broker = broker;
            this.dbOprService = dbOprService;
            this.schedule = schedule;
        }
        public TimingBase(IBroker broker, IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            this.broker = broker;
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
            this.schedule = schedule;
        }


        public abstract JobDataAddResponseModel Timing(JobDataDbo jobData);
    }
}
