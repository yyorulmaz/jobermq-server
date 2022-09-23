using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using System;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class DfQueuePriority : QueueBase
    {
        IQueueChildDataBasePriority Data;
        public DfQueuePriority(string distributorName, string queueName, MatchTypeEnum matchType, SendTypeEnum sendType, IClientGroup clientGroup, IQueueDataBase queueDataBase) : base(distributorName, queueName, matchType, sendType, clientGroup, queueDataBase)
        {

        }

        public override JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
