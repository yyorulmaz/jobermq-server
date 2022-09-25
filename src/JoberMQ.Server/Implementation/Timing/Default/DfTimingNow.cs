using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.Timing.Default
{
    internal class DfTimingNow : TimingBase
    {
        public DfTimingNow(IDbOprService dbOprService, IDboCreator dboCreator) : base(dbOprService, dboCreator)
        {
        }

        public override JobDataAddResponseModel Timing(JobDataDbo jobData)
        {
            dbOprService.JobData.Add(jobData);
            var createdJobDbo = dboCreator.JobDboCreate(jobData);
            dbOprService.Job.Add(createdJobDbo);

            var createdMessageDbos = dboCreator.MessageDboCreates(createdJobDbo);

            foreach (var item in createdMessageDbos)
            {
                // burada tek tek brokıra göndericem
                // veya toplu gönderme içinde bir method yazıcam
            }



            return null;
        }
    }
}
