﻿using JoberMQ.Data.Abstraction.Repository.DbText;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.DataAccess.Abstract.DBTEXT
{
    internal interface IUserTextDal : IDbTextRepository<UserDbo>
    {
    }
}
