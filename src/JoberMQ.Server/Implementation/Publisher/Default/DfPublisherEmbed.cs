using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

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

            var isSuccess = dbOprService.JobData.Add(jobData);

            if (isSuccess)
            {
                dbOprService.JobData.Commit(jobData);
                response.IsSuccess = true;
            }
            else
            {
                dbOprService.JobData.Rollback(jobData);
                response.IsSuccess = false;
                response.Message = "error"; // todo error statuscode ekle
            }
            
            response.JobId = jobData.Id;
            return response;
        }
    }
}
