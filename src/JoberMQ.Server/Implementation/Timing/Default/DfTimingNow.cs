using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Broker;
using JoberMQ.Server.Abstraction.DboCreator;
using JoberMQ.Server.Abstraction.DbOpr;
using System;

namespace JoberMQ.Server.Implementation.Timing.Default
{
    internal class DfTimingNow : TimingBase
    {
        public DfTimingNow(IBroker broker, IDbOprService dbOprService, IDboCreator dboCreator) : base(broker, dbOprService, dboCreator)
        {
        }

        public override JobDataAddResponseModel Timing(JobDataDbo jobData)
        {
            var response = new JobDataAddResponseModel();
            response.IsOnline = true;
            response.JobId = jobData.Id;

            var addJobDataResult = dbOprService.JobData.Add(jobData);
            if (!addJobDataResult)
            {
                response.IsSuccess = false;
                response.Message = "JobData eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdJobDbo = dboCreator.JobDboCreate(jobData);
            var addJobResult = dbOprService.Job.Add(createdJobDbo);
            if (!addJobResult)
            {
                dbOprService.JobData.Rollback(jobData);

                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdMessageDbos = dboCreator.MessageDboCreates(createdJobDbo);
            var queueAddResult = broker.QueueAdd(createdMessageDbos);
            if (!queueAddResult)
            {
                dbOprService.JobData.Rollback(jobData);
                dbOprService.Job.Rollback(createdJobDbo);

                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}
