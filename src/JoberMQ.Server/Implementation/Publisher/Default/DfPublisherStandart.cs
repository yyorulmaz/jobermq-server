using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Schedule;
using System;

namespace JoberMQ.Server.Implementation.Publisher.Default
{
    internal class DfPublisherStandart : PublisherBase
    {
        public DfPublisherStandart(IDbOprService dbOprService, IDboCreator dboCreator, ISchedule schedule) : base(dbOprService, dboCreator, schedule)
        {
        }

        public override JobDataAddResponseModel Publish(JobDataDbo jobData)
             //=> TimingFactory.CreateTimingService(jobData.TimingType, dbOprService, dboCreatorService, scheduleService).Timing(jobData);
        {

            throw new NotImplementedException();
        }
    }
}
