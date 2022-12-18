using System.Collections.Concurrent;

namespace JoberMQ.Data.Abstraction.Repository.DbMem
{
    internal interface IDbChildMemLIFORepository<TKey, TValue> : IDbChildMemRepository<TKey, TValue>
    {
        #region Data
        ConcurrentStack<TValue> ChildData { get; }
        #endregion
    }
}
