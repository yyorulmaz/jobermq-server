using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Enums.Consume;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace JoberMQ.Client.Implementation.Default
{
    public class DfClientChildData : IClientChildData
    {
        #region Constructor
        //public DfClientChildData(IClientMasterData clientMasterData, ConsumeTypeEnum consumeType, string consumeKey)
        public DfClientChildData(IClientMasterData clientMasterData, string consumeKey)
        {
            this.clientMasterData = clientMasterData;
            databaseLocal = new ConcurrentDictionary<string, IClient>();
            //this.consumeType = consumeType;
            this.consumeKey = consumeKey;

            this.clientMasterData.ChangedAdded += MasterData_ChangedAdded;
            this.clientMasterData.ChangedUpdated += MasterData_ChangedUpdated;
            this.clientMasterData.ChangedRemoved += MasterData_ChangedRemoved;
        }
        #endregion

        ConsumeTypeEnum consumeType;
        string consumeKey;

        #region Data
        private IClientMasterData clientMasterData;
        public IClientMasterData ClientMasterData => clientMasterData;


        private readonly ConcurrentDictionary<string, IClient> databaseLocal;
        public ConcurrentDictionary<string, IClient> DatabaseLocal => databaseLocal;
        #endregion

        #region Count
        public int Count => databaseLocal.Count;
        #endregion

        #region CRUD
        public IClient Get(string key)
        {
            databaseLocal.TryGetValue(key, out IClient value);
            return value;
        }
        public IClient Get(Func<IClient, bool> filter)
        {
            return databaseLocal.Values.FirstOrDefault(filter);
        }

        public bool Update(string key, IClient value)
        {
            clientMasterData.Update(key, value);
            var result = databaseLocal.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(key, value);
            return result;
        }
        public IClient Remove(string key, IClient value)
        {
            clientMasterData.Remove(key);
            databaseLocal.TryRemove(key, out IClient rsltvalue);
            if (rsltvalue != null)
                ChangedRemoved?.Invoke(key, value);
            return rsltvalue;
        }
        #endregion

        #region Changed
        public event Action<string, IClient> ChangedAdded;
        public event Action<string, IClient> ChangedUpdated;
        public event Action<string, IClient> ChangedRemoved;
        #endregion

        #region MasterToChild and ChildToMaster
        private void MasterData_ChangedAdded(string key, IClient value)
        {
            //todo kontrol
            //var check = value.Consuming.FirstOrDefault(x => x.Value.ConsumeType == consumeType && x.Value.DeclareKey == consumeKey);
            var check = value.Consuming.FirstOrDefault(x => x.Value.DeclareKey == consumeKey);
            databaseLocal.TryGetValue(key, out var childClientCheck);
            if (check.Value != null && childClientCheck == null)
            {
                var rslt = databaseLocal.TryAdd(key, value);
                if (rslt)
                    ChangedAdded?.Invoke(key, value);
            }
        }
        private void MasterData_ChangedUpdated(string key, IClient value)
        {
            var check = value.Consuming.FirstOrDefault(x => x.Value.DeclareKey == consumeKey);
            databaseLocal.TryGetValue(key, out var childClientCheck);
            if (check.Value != null && childClientCheck == null)
            {
                var rslt = databaseLocal.TryAdd(key, value);
                if (rslt)
                    ChangedAdded?.Invoke(key, value);
            }
            else
            {
                var rslt = databaseLocal.TryUpdate(key, value, value);
                if (rslt)
                    ChangedUpdated?.Invoke(key, value);
            }



            
        }
        private void MasterData_ChangedRemoved(string key, IClient value)
        {
            databaseLocal.TryRemove(key, out IClient outvalue);
            if (outvalue != null)
                ChangedRemoved?.Invoke(key, outvalue);
        }
        #endregion
    }
}
