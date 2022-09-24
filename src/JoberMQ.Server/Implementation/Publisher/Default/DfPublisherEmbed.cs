using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;

namespace JoberMQ.Server.Implementation.Publisher.Default
{
    internal class DfPublisherEmbed : PublisherBase
    {
        public DfPublisherEmbed(IDbOprService dbOprService) : base(dbOprService)
        {
        }

        public override JobDataAddResponseModel Publish(JobDataDbo jobData)
        {
            var response = new JobDataAddResponseModel();
            response.IsOnline = true;
            response.IsSuccess = dbOprService.JobData.Add(jobData);
            response.JobId = jobData.Id;
            return response;
        }
    }
}
