using System.Collections.Concurrent;

namespace JoberMQ.Data.Abstraction.Repository.DbMem
{
    internal interface IDbChildMemGeneralRepository<TKey, TValue> : IDbChildMemRepository<TKey, TValue>
    {
        #region Data
        ConcurrentDictionary<TKey, TValue> ChildData { get; }
        #endregion
    }
}
