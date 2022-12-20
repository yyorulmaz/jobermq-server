using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Database.Abstraction.Repository.DbOpr;
using JoberMQ.Database.Abstraction.Repository.DbText;
using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Data;
using JoberMQ.Entities.Helper;
using System;
using System.Collections.Generic;

namespace JoberMQ.Database.Implementation.Repository.DbOpr.Default
{
    internal class DfDbOprRepository<D> : IDbOprRepository<D>
        where D : DboPropertyGuidBase, new()
    {
        private readonly IDbMemRepository<Guid, D> dbMem;
        private readonly IDbTextRepository<D> dbText;
        internal DfDbOprRepository(
            IDbMemRepository<Guid, D> dbMem,
            IDbTextRepository<D> dbText)
        {
            this.dbMem = dbMem;
            this.dbText = dbText;
        }

        public IDbMemRepository<Guid, D> DbMem => dbMem;
        public IDbTextRepository<D> DbText => dbText;

        #region CRUD
        public D Get(Guid id) => dbMem.Get(id);
        public List<D> GetAll(Func<D, bool> filter = null) => dbMem.GetAll(filter);
        public virtual bool Add(D dbo)
        {
            var processTime = DateHelper.GetUniversalNow();
            dbo.CreateDate = processTime;
            dbo.DataStatusType = DataStatusTypeEnum.Insert;
            dbo.ProcessTime = processTime;

            dbText.WriteLine(dbo);
            dbMem.Add(dbo.Id, dbo);
            ChangedAdded?.Invoke(dbo);

            return true;
        }
        public virtual bool Update(D dbo)
        {
            var processTime = DateHelper.GetUniversalNow();
            dbo.UpdateDate = processTime;
            dbo.DataStatusType = DataStatusTypeEnum.Update;
            dbo.ProcessTime = processTime;

            dbText.WriteLine(dbo);
            dbMem.Update(dbo.Id, dbo);
            ChangedUpdated?.Invoke(dbo);

            return true;
        }
        public virtual bool Delete(D dbo)
        {
            var processTime = DateHelper.GetUniversalNow();
            dbo.DataStatusType = DataStatusTypeEnum.Delete;
            dbo.ProcessTime = processTime;

            dbText.WriteLine(dbo);
            dbMem.Remove(dbo.Id);
            ChangedRemoved?.Invoke(dbo);

            return true;
        }

        public bool Commit(D dbo)
        {
            dbo.IsTransactionCompleted = true;
            dbo.TransactionDate = DateHelper.GetUniversalNow();

            return Add(dbo);
        }
        public bool Rollback(D dbo)
        {
            dbo.IsTransactionCompleted = false;
            dbo.TransactionDate = DateHelper.GetUniversalNow();

            return Delete(dbo);
        }
        #endregion

        #region Changed
        public event Action<D> ChangedAdded;
        public event Action<D> ChangedUpdated;
        public event Action<D> ChangedRemoved;
        #endregion

        public bool ImportTextDataToSetMemDb()
        {
            var datas = dbText.ReadAllDataGrouping(true);

            if (datas != null)
                foreach (var data in datas)
                    dbMem.Add(data.Id, data);

            return true;
        }

        public bool CreateDatabase()
            => dbText.CreateDatabase();
        public bool Setup()
            => dbText.Setup();

        public bool DataGroupingAndSize()
            => dbText.DataGroupingAndSize();

        public int ArsiveFileCounter { get => dbText.ArsiveFileCounter; set => dbText.ArsiveFileCounter = value; }
    }
}
