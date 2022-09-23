﻿using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Response;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Server.Implementation.Queue
{
    internal class DfQueueFIFO : QueueBase
    {
        ConcurrentQueue<MessageDbo> Data;
        public DfQueueFIFO(string distributorName, string queueName, MatchTypeEnum matchType, SendTypeEnum sendType, IClientGroup clientGroup, IQueueDataBase queueDataBase) : base(distributorName, queueName, matchType, sendType, clientGroup, queueDataBase)
        {
            Data = new ConcurrentQueue<MessageDbo>();
        }

        public override JobDataAddResponseModel QueueAdd(MessageDbo message)
        {
            throw new NotImplementedException();
        }
    }
}
