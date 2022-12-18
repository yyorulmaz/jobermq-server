using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Publisher;
using JoberMQ.Server.Abstraction.Timing;

namespace JoberMQ.Server.Implementation.Publisher
{
    internal abstract class PublisherBase : IPublisher
    {
        protected readonly ServerConfigModel serverConfig;
        protected readonly IBroker broker;
        protected readonly IDbOprService dbOprService;
        protected readonly IDboCreator dboCreator;
        protected readonly ISchedule schedule;
        public PublisherBase(IDbOprService dbOprService)
        {
            this.dbOprService = dbOprService;
        }
        public PublisherBase(ServerConfigModel serverConfig, IBroker broker, IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule)
        {
            this.serverConfig = serverConfig;
            this.broker = broker;
            this.dbOprService = dbOprService;
            this.dboCreator = dboCreator;
            this.schedule = schedule;
        }

        public abstract JobAddResponseModel Publish(JobDbo job);
    }
}
