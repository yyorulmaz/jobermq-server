using JoberMQ.Data.Abstraction.Repository.DbMem;
using JoberMQ.Data.Abstraction.Repository.DbText;
using JoberMQ.Entities.Base.Dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Data.Abstraction.Repository.DbOpr
{
    internal interface IDbOprRepository<D>
    where D : DboPropertyGuidBase, new()
    {
        public IDbMemRepository<Guid, D> DbMem { get; }
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
