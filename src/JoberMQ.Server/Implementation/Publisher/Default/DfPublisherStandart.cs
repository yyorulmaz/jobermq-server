using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Timing;
using JoberMQ.Server.Factories.Timing;

namespace JoberMQ.Server.Implementation.Publisher.Default
{
    internal class DfPublisherStandart : PublisherBase
    {
        public DfPublisherStandart(ServerConfigModel serverConfig, IBroker broker, IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule) : base(serverConfig, broker, dbOprService, dboCreator, schedule)
        {
        }

        public override JobDataAddResponseModel Publish(JobDataDbo jobData)
             => TimingFactory.CreateTiming(serverConfig.TimingConfig.TimingFactory, jobData.TimingType, broker, dbOprService, dboCreator, schedule).Timing(jobData);
    }
}
