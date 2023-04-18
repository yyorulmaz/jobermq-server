using JoberMQ.Abstraction.Jober;
using JoberMQ.Implementation.Jober.Default;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.Models.Rpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace JoberMQ.Hubs
{
    internal class JoberHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var result = JoberHost.Jober.ConnectedOperation(Context).Result;
            if (result == false)
            {
                var errorMessage = JoberHost.Jober.StatusCode.GetStatusMessage("0.0.14");
                return base.OnDisconnectedAsync(new System.Exception(errorMessage));
            }
            else
            {
                return base.OnConnectedAsync();
            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var result = JoberHost.Jober.DisConnectedOperation(Context).Result;
            return base.OnDisconnectedAsync(exception);
        }



        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Distributor(string distributorData)
            => await JoberHost.Jober.DistributorOperation(distributorData);

        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Queue(string queueData)
            => await JoberHost.Jober.QueueOperation(queueData);

        [Authorize(Roles = "administrators")]
        public async Task<ResponseModel> Consume(string consumeData)
            => await JoberHost.Jober.ConsumeOperation(Context.ConnectionId, consumeData);




        public async Task<ResponseModel> Job(string job)
            => await JoberHost.Jober.JobOperation(job);

        public async Task<ResponseModel> Message(string message)
            => await JoberHost.Jober.MessageOperation(message);

        public async Task<RpcResponseModel> Rpc(string rpc)
            => await JoberHost.Jober.RpcOperation(rpc);
        public async Task RpcResponse(string rpc)
            => await JoberHost.Jober.RpcResponseOperation(rpc);


        public async Task<ResponseModel> MessageStarted(string data)
            => await JoberHost.Jober.MessageStartedOperation(data);

        public async Task<ResponseModel> MessageCompleted(string data)
           => await JoberHost.Jober.MessageCompletedOperation(data);

    }


    public interface IChannel<T> : IDisposable
    {

    }
    public class DfChannel<T> : IChannel<T>
    {
        Channel<T> channel;
        string id;
        public DfChannel(string id)
        {
            this.id = id;
            channel = Channel.CreateUnbounded<T>();
        }

        #region Dispose
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
                disposedValue=true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DfChannel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    public enum ChannelFactoryEnum
    {
        Default = 1
    }
    internal class ChannelFactory
    {
        public static IChannel<T> Create<T>(ChannelFactoryEnum channelFactory, string id)
        {
            IChannel<T> result;

            switch (channelFactory)
            {
                case ChannelFactoryEnum.Default:
                    result = new DfChannel<T>(id);
                    break;
                default:
                    result = new DfChannel<T>(id);
                    break;
            }

            return result;
        }
    }

}
