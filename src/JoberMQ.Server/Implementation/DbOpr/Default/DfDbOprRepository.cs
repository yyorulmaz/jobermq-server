using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.DataAccess.Repository.DbText.Abstraction;
using JoberMQ.Entities.Base.Dbo;
using JoberMQ.Entities.Enums.Data;
using JoberMQ.Entities.Helper;
using JoberMQ.Server.Abstraction.DbOpr;
using System;
using System.Collections.Generic;

namespace JoberMQ.Server.Implementation.DbOpr.Default
{
    internal class DfDbOprRepository<D> : IDbOprRepository<D>
        where D : DboPropertyGuidBase, new()
    {
        private readonly IConcurrentDictionaryRepository<Guid, D> dbMem;
        private readonly IDbTextRepository<D> dbText;
        internal DfDbOprRepository(
            IConcurrentDictionaryRepository<Guid, D> dbMem,
            IDbTextRepository<D> dbText)
        {
            this.dbMem = dbMem;
            this.dbText = dbText;
        }

        public IConcurrentDictionaryRepository<Guid, D> DbMem => dbMem;
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
        public bool Setup()
            => dbText.Setup();

        public bool DataGroupingAndSize()
            => dbText.DataGroupingAndSize();

        public int ArsiveFileCounter { get => dbText.ArsiveFileCounter ; set => dbText.ArsiveFileCounter = value; }
    }
}
