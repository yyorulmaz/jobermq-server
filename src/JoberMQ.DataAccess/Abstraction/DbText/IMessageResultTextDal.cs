﻿using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.DataAccess.Abstract.DBTEXT
{
    internal interface IMessageResultTextDal : IDbTextRepository<MessageResultDbo>
    {
    }
}
