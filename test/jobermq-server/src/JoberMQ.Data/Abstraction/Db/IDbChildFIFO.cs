using System.Collections.Concurrent;

namespace JoberMQ.Data.Abstraction.Db
{
    internal interface IDbChildFIFO<TKey, TValue> : IDbChild<TKey, TValue>
    {
        #region Data
        ConcurrentQueue<TValue> ChildData { get; }
        #endregion
    }
}
