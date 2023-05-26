using JoberMQ.Common.Enums.Client;
using JoberMQ.Abstraction.Client;

namespace JoberMQ.Implementation.Client.Default
{
    internal class DefaultClient : IClient
    {
        public DefaultClient(string connectionId, string clientKey, ClientTypeEnum clientType)
        {
            this.connectionId = connectionId;
            this.clientKey = clientKey;
            this.clientType = clientType;
        }

        readonly string connectionId;
        public string ConnectionId => connectionId;

        readonly string clientKey;
        public string ClientKey => clientKey;


        bool isActive = false;
        public bool IsActive { get => isActive; set => isActive = value; }

        ClientTypeEnum clientType;
        public ClientTypeEnum ClientType { get => clientType; set => clientType = value; }

        int number;
        public int Number { get => number; set => number = value; }

        string[] tags;
        public string[] Tags { get => tags; set => tags = value; }

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

        void System.IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
        #endregion
    }

}
