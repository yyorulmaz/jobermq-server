﻿using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using System;

namespace JoberMQ.Database.Abstraction.DbMem
{
    internal interface IMessageDbMem : IMemRepository<Guid, MessageDbo>
    {
    }
}
