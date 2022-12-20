using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.Database.Abstraction.DbMem
{
    internal interface IJobTransactionMemDal : IDbMemRepository<Guid, JobTransactionDbo>
    {
    }
}
