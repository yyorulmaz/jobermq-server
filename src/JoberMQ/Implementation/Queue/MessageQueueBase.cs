using JoberMQ.Abstraction.Client;
using JoberMQ.Abstraction.Queue;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Models.Response;
using System.Collections.Concurrent;

namespace JoberMQ.Implementation.Queue
{
    internal abstract class MessageQueueBase : IMessageQueue
    {
        public MessageQueueBase(
            string queueKey,
            string[] tags,
            QueueMatchTypeEnum queueMatchType, 
            QueueOrderOfSendingTypeEnum queueOrderOfSendingType, 
            PermissionTypeEnum permissionType, 
            bool isDurable,
            bool isDefault,
            bool isActive)
        {
            this.queueKey = queueKey;
            this.tags = tags;
            this.queueMatchType = queueMatchType;
            this.queueOrderOfSendingType = queueOrderOfSendingType;
            this.permissionType = permissionType;
            this.isDurable = isDurable;
            this.isDefault = isDefault;
            this.isActive = isActive;

            this.clientChildData = new ConcurrentDictionary<string, IClient>();

            JoberHost.JoberMQ.Clients.ChangedAdded += Clients_ChangedAdded;
            JoberHost.JoberMQ.Clients.ChangedUpdated += Clients_ChangedUpdated;
            JoberHost.JoberMQ.Clients.ChangedRemoved += Clients_ChangedRemoved;

            foreach (var client in JoberHost.JoberMQ.Clients.Database.ToArray())
            {
                var check = JoberHost.JoberMQ.Database.Subscript.Get(x => x.QueueKey == queueKey && x.IsActive == true && x.ClientKey == client.Value.ClientKey);
                if (check == null)
                    return;

                clientChildData.TryAdd(client.Key, client.Value);
            }
        }

        #region IMessageQueue Property
        protected string queueKey;
        public string QueueKey { get => queueKey; set => queueKey = value; }

        protected string[] tags;
        public string[] Tags { get => tags; set => tags = value; }

        protected QueueMatchTypeEnum queueMatchType;
        public QueueMatchTypeEnum QueueMatchType { get => queueMatchType; set => queueMatchType = value; }

        protected QueueOrderOfSendingTypeEnum queueOrderOfSendingType;
        public QueueOrderOfSendingTypeEnum QueueOrderOfSendingType { get => queueOrderOfSendingType; set => queueOrderOfSendingType = value; }

        protected PermissionTypeEnum permissionType;
        public PermissionTypeEnum PermissionType { get => permissionType; set => permissionType = value; }

        protected bool isDurable;
        public bool IsDurable { get => isDurable; set => isDurable = value; }
        protected bool isDefault;
        public bool IsDefault { get => isDefault; set => isDefault = value; }

        protected bool isActive = true;
        public bool IsActive { get => isActive; set => isActive = value; }
        #endregion

        #region Clients_Changed
        private void Clients_ChangedAdded(string conectionId, IClient client)
        {
            var check = JoberHost.JoberMQ.Database.Subscript.Get(x => x.QueueKey == queueKey && x.IsActive == true && x.ClientKey == client.ClientKey);
            if (check == null)
                return;

            clientChildData.TryAdd(conectionId, client);
            SendOperation();
        }
        private void Clients_ChangedUpdated(string conectionId, IClient client)
        {
            clientChildData.TryGetValue(conectionId, out var check);
            if (check == null)
                return;

            clientChildData.TryUpdate(conectionId, client, null);
            SendOperation();
        }
        private void Clients_ChangedRemoved(string conectionId, IClient client)
        {
            clientChildData.TryRemove(conectionId, out var xxxx);
            SendOperation();
        }
        #endregion

        #region MessageAdd
        protected abstract Task<bool> ChildMessageAdd(MessageDbo message);
        public async Task<ResponseModel> Queueing(MessageDbo message)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            message.Message.Routing.QueueKey = queueKey;

            if (message.IsDbTextSave)
            {
                var addMessage = JoberHost.JoberMQ.Database.Message.Add(message.Id, message);
                if (addMessage)
                {
                    var addMessageChild = await ChildMessageAdd(message);
                    if (addMessageChild)
                        result.IsSucces = true;
                    else
                    {
                        JoberHost.JoberMQ.Database.Message.Delete(message.Id, message);
                        result.IsSucces = false;
                    }
                }
                else
                {
                    result.IsSucces = false;
                }
            }
            else
            {
                var addMessageChild = await ChildMessageAdd(message);
                if (addMessageChild)
                    result.IsSucces = true;
                else
                    result.IsSucces = false;
            }


            

            return result;
        }
        #endregion

        







        protected bool isSendRuning;
        public bool IsSendRuning { get => isSendRuning; set => isSendRuning = value; }


        protected ConcurrentDictionary<string, IClient> clientChildData;


        protected int endConsumerNumber = 0;
        public abstract int ChildMessageCount { get; }
        
        protected abstract void SendOperation();
        protected abstract void Qperation();
    }
}
