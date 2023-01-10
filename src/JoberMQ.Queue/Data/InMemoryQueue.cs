﻿using JoberMQ.Common.Dbos;
using JoberMQ.Queue.Abstraction;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Queue.Data
{
    internal class InMemoryQueue
    {
        internal static ConcurrentDictionary<string, IMessageQueue> MessageQueuesData = new ConcurrentDictionary<string, IMessageQueue>();
    }
}