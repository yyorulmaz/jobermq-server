using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JoberMQ.Abstraction.Client;

namespace JoberMQ.Implementation.Client.Default
{
    internal class DefaultClients : IClients
    {
        #region Constructor
        public DefaultClients()
        {
            database = new ConcurrentDictionary<string, IClient>();
        }
        #endregion

        #region Data
        private readonly ConcurrentDictionary<string, IClient> database;
        public ConcurrentDictionary<string, IClient> Database => database;
        #endregion

        #region Count
        public int Count => database.Count;
        #endregion

        #region CRUD
        public IClient Get(string key)
        {
            database.TryGetValue(key, out IClient value);
            return value;
        }
        public IClient Get(Func<IClient, bool> filter)
        {
            return database.Values.FirstOrDefault(filter);
        }
        public List<IClient> GetAll(Func<IClient, bool> filter = null)
        {
            if (filter == null)
                return database.Values.ToList();
            else
                return database.Values.Where(filter).ToList();
        }
        public bool Add(string key, IClient value)
        {
            var result = database.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(key, value);
            return result;
        }
        public bool Update(string key, IClient value)
        {
            var result = database.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(key, value);
            return result;
        }
        public IClient Remove(string key)
        {
            database.TryRemove(key, out IClient value);
            if (value != null)
                ChangedRemoved?.Invoke(key, value);
            return value;
        }
        #endregion

        #region Changed
        public void InvokeChangedAdded(string connectionId)
            => ChangedAdded?.Invoke(connectionId, Get(connectionId));
        public void InvokeChangedUpdated(string connectionId)
            => ChangedUpdated?.Invoke(connectionId, Get(connectionId));
        public void InvokeChangedRemoved(string connectionId)
            => ChangedRemoved?.Invoke(connectionId, Get(connectionId));
        public event Action<string, IClient> ChangedAdded;
        public event Action<string, IClient> ChangedUpdated;
        public event Action<string, IClient> ChangedRemoved;
        #endregion

        #region Disposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DefaultClient()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
        #endregion
    }
}
