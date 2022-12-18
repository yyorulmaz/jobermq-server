using System.Collections.Concurrent;

namespace JoberMQ.Data.Abstraction.Repository.DbMem
{
    internal interface IDbChildMemFIFORepository<TKey, TValue> : IDbChildMemRepository<TKey, TValue>
    {
        #region Data
        ConcurrentQueue<TValue> ChildData { get; }
        #endregion
    }
}
