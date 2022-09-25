using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Distributor;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server
{
    public static class QueueType
    {
        internal const string Special = "Special";
        internal const string Group = "Group";
        internal const string Free = "Free";
    }



    

}
