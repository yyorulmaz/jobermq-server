using System.Collections.Concurrent;

namespace JoberMQ.Data.Abstraction.Db
{
    internal interface IDbChildPriority<TKey, TValue> : IDbChild<TKey, TValue>
    {
        #region Data
        ConcurrentDictionary<TKey, TValue> ChildData { get; }
        #endregion
    }
}
