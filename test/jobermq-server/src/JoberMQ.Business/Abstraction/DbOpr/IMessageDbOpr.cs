﻿using JoberMQ.Data.Abstraction.Repository.DbOpr;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.Business.Abstraction.DbOpr
{
    internal interface IMessageDbOpr : IDbOprRepository<MessageDbo>
    {
    }
}
