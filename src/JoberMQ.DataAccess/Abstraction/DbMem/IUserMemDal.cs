using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using System;

namespace JoberMQ.DataAccess.Abstract.DbMem
{
    internal interface IUserMemDal : IConcurrentDictionaryRepository<Guid, UserDbo>
    {
    }
}
