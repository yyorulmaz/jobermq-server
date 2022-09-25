using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Base.Dbo;
using System;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.DbOpr
{
    internal interface IDbOprRepository<D>
    where D : DboPropertyGuidBase, new()
    {
        public IConcurrentDictionaryRepository<Guid, D> DbMem { get; }
        public IDbTextRepository<D> DbText { get; }


        #region CRUD
        public D Get(Guid id);
        public List<D> GetAll(Func<D, bool> filter = null);
        public bool Add(D dbo);
        public bool Update(D dbo);
        public bool Delete(D dbo);

        public bool Commit(D dbo);
        public bool Rollback(D dbo);
        #endregion

        #region Changed
        public event Action<D> ChangedAdded;
        public event Action<D> ChangedUpdated;
        public event Action<D> ChangedRemoved;
        #endregion

        public bool ImportTextDataToSetMemDb();
        
        public bool CreateDatabase();
        public bool Setup();
        public bool DataGroupingAndSize();
        int ArsiveFileCounter { get; set; }
    }
}
