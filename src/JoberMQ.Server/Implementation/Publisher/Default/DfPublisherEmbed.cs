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

        public override JobAddResponseModel Publish(JobDbo job)
        {
            var response = new JobAddResponseModel();
            response.IsOnline = true;

            var isSuccess = dbOprService.Job.Add(job);

            if (isSuccess)
            {
                dbOprService.Job.Commit(job);
                response.IsSuccess = true;
            }
            else
            {
                dbOprService.Job.Rollback(job);
                response.IsSuccess = false;
                response.Message = "error"; // todo error statuscode ekle
            }
            
            response.JobId = job.Id;
            return response;
        }
    }
}
