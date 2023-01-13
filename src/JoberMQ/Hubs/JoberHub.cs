using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace JoberMQ.Hubs
{
    internal class JoberHub : Hub
    {
        //todo CLIENT CONNECT OLDUĞUNDA CLIENTGROUPKEY İNE GÖRE KUYRUK OLUŞTURMA DURUMU












        internal async Task<bool> Job()
        {

            return true;
        }
        internal async Task<bool> Message()
        {


            return true;
        }
        internal async Task<bool> Rpc()
        {


            return true;
        }
    }
}
