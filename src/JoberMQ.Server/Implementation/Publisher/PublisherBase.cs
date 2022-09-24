using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Publisher;
using JoberMQ.Server.Abstraction.Schedule;

namespace JoberMQ.Server.Implementation.Publisher
{
    internal abstract class PublisherBase : IPublisher
    {
        protected readonly IDbOprService dbOprService;
        protected readonly IDboCreator dboCreator;
        protected readonly ISchedule schedule;
        public PublisherBase(IDbOprService dbOprService)
        {
            this.dbOprService = dbOprService;
        }
        public PublisherBase(IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
            this.schedule = schedule;
        }

        public abstract JobDataAddResponseModel Publish(JobDataDbo jobData);
    }
}
