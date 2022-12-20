using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.Database.Abstraction.DbMem
{
    internal interface IMessageResultMemDal : IDbMemRepository<Guid, MessageResultDbo>
    {
    }
}
