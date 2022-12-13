using System.Collections.Concurrent;

namespace JoberMQ.Data.Abstraction.Db
{
    internal interface IDbChildLIFO<TKey, TValue> : IDbChild<TKey, TValue>
    {
        #region Data
        ConcurrentStack<TValue> ChildData { get; }
        #endregion
    }
}
