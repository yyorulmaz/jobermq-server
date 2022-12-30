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

        public override JobAddResponseModel Timing(JobDbo job)
        {
            var response = new JobAddResponseModel();
            response.IsOnline = true;
            response.JobId = job.Id;

            var addJobResult = dbOprService.Job.Add(job);
            if (!addJobResult)
            {
                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdJobDbo = dboCreator.JobTransactionDboCreate(job);
            var addJobTransactionResult = dbOprService.JobTransaction.Add(createdJobDbo);
            if (!addJobTransactionResult)
            {
                dbOprService.Job.Rollback(job);

                response.IsSuccess = false;
                response.Message = "JobTransaction eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }



            var createdMessageDbos = dboCreator.MessageDboCreates(createdJobDbo);
            var queueAddResult = broker.QueueAdd(createdMessageDbos);
            if (!queueAddResult)
            {
                dbOprService.Job.Rollback(job);
                dbOprService.JobTransaction.Rollback(createdJobDbo);

                response.IsSuccess = false;
                response.Message = "Job eklenemedi, işlemler geri alındı."; // todo statuscode
                return response;
            }

            response.IsSuccess = true;
            return response;
        }
    }
}
