using GenRep.ConcurrentRepository.ConcurrentDictionary;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQNEW.Server.Abstraction.Client;
using System;

namespace JoberMQ.Server.Abstraction.Queue
{
    internal interface IQueue
    {
        public string DistributorName { get; }
        public string QueueName { get; }
        public MatchTypeEnum MatchType { get; }
        public SendTypeEnum SendType { get; }
        public IClientGroup ClientGroup { get; }
        public IConcurrentDictionaryChildRepository<Guid, MessageDbo> Messages { get; }
        public bool IsSendRuning { get; set; }
        public JobDataAddResponseModel QueueAdd(MessageDbo message);
    }
}
