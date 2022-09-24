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
            //todo yap
            throw new NotImplementedException();
        }
    }
}
