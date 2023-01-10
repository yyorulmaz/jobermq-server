﻿using JoberMQ.Common.Enums.Jober;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationJober : IConfigurationJober
    {
        JoberFactoryEnum joberFactory = DefaultJoberConst.JoberFactory;
        public JoberFactoryEnum JoberFactory { get => joberFactory; set => joberFactory = value; }
        bool isJoberActive = DefaultJoberConst.IsJoberActive;
        public bool IsJoberActive { get => isJoberActive; set => isJoberActive = value; }
    }
}