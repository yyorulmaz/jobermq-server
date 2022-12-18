using JoberMQ.Data.Abstraction.Repository.DbMem;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.DataAccess.Abstract.DbMem
{
    internal interface IMessageMemDal : IDbMemRepository<Guid, MessageDbo>
    {
    }
}
