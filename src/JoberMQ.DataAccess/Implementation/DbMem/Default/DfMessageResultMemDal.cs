﻿using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Abstract.DbMem;
using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.DataAccess.Implementation.DbMem.Default
{
    internal class DfMessageResultMemDal : ConcurrentDictionaryRepository<Guid, MessageResultDbo>, IMessageResultMemDal
    {
        public DfMessageResultMemDal()
        {
        }

        public DfMessageResultMemDal(ConcurrentDictionary<Guid, MessageResultDbo> data) : base(data)
        {
        }
    }
}
