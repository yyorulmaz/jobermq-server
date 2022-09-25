using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Broker;
using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Enums.DbOpr;
using JoberMQ.Entities.Enums.Distributor;
using JoberMQ.Entities.Enums.Permission;
using JoberMQ.Entities.Enums.Publisher;
using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Enums.Schedule;
using JoberMQ.Entities.Enums.Server;
using JoberMQ.Entities.Enums.StatusCode;
using JoberMQ.Entities.Enums.Timing;
using System;
using System.Collections.Concurrent;

namespace JoberMQ.Entities.Models.Config
{
    public class ServerConfigModel
    {
        internal ServerFactoryEnum ServerFactory => ServerConst.ServerFactory;
        internal ClientFactoryEnum ClientFactory => ServerConst.ClientFactory;
        internal ClientGroupFactoryEnum ClientGroupFactory => ServerConst.ClientGroupFactory;
        internal ClientServiceFactoryEnum ClientServiceFactory => ServerConst.ClientServiceFactory;

        public StatusCodeConfigModel StatusCodeConfig => new StatusCodeConfigModel();
        public SecurityConfigModel SecurityConfig => new SecurityConfigModel();
        public DbOprConfigModel DbOprConfig => new DbOprConfigModel();
        internal BrokerConfigModel BrokerConfig => new BrokerConfigModel();
        public HostConfigModel HostConfig => new HostConfigModel();
        public TimingConfigModel TimingConfig => new TimingConfigModel();
        public PublisherConfigModel PublisherConfig => new PublisherConfigModel();
    }

    public class StatusCodeConfigModel
    {
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage => ServerConst.StatusCode.StatusCodeMessageLanguage;
    }
    public class SecurityConfigModel
    {
        public string SecurityKey { get; set; }
    }
    public class DbOprConfigModel
    {
        internal DbOprServiceFactoryEnum DbOprServiceFactory => ServerConst.DbOpr.DbOprServiceFactory;
        internal DbOprFactoryEnum DbOprFactory => ServerConst.DbOpr.DbOprFactory;
        internal DbMemConfigModel DbMemConfig => new DbMemConfigModel();
        internal DbTextConfigModel DbTextConfig => new DbTextConfigModel();
        internal DboCreatorFactoryEnum DboCreatorFactory => ServerConst.DbOpr.DboCreatorFactory;
        public string CompletedDataRemovesTimer => ServerConst.DbOpr.CompletedDataRemovesTimer;
    }
    public class DbMemConfigModel
    {
        internal DbMemFactoryEnum DbMemFactory => ServerConst.DbOpr.DbMemFactory;
        internal DbMemDataFactoryEnum DbMemDataFactory => ServerConst.DbOpr.DbMemDataFactory;
    }
    public class DbTextConfigModel
    {
        internal DbTextFactoryEnum DbTextFactory => ServerConst.DbOpr.DbTextFactory;
        internal ConcurrentDictionary<string, DbTextFileConfigModel> DbTextFileConfigDatas = ServerConst.DbOpr.DbTextFileConfigDatas;
    }
    public class DbTextFileConfigModel
    {
        public string DbPath { get; set; }
        public string DbFolderPath { get; set; }
        public string DbFileName { get; set; }
        public char DbFileSeparator { get; set; }
        public char DbArchiveFileSeparator { get; set; }
        public string DbFileExtension { get; set; }
        public int MaxRowCount { get; set; }
    }
    public class BrokerConfigModel
    {
        internal BrokerFactoryEnum BrokerFactory => ServerConst.Broker.BrokerFactory;
        internal QueueFactoryEnum QueueFactory => ServerConst.Broker.QueueFactory;
        internal QueueChildPriorityFactoryEnum QueueChildPriorityFactory => ServerConst.Broker.QueueChildPriorityFactory;
        internal QueueChildFIFOFactoryEnum QueueChildFIFOFactory => ServerConst.Broker.QueueChildFIFOFactory;
        internal QueueChildLIFOFactoryEnum QueueChildLIFOFactory => ServerConst.Broker.QueueChildLIFOFactory;
        internal DistributorFactoryEnum DistributorFactory => ServerConst.Broker.DistributorFactory;
    
        internal ConcurrentDictionary<string, DefaultDistributorConfigModel> DefaultDistributorConfigDatas = ServerConst.Broker.DefaultDistributorConfigDatas;
        internal ConcurrentDictionary<string, DefaultQueueConfigModel> DefaultQueueConfigDatas = ServerConst.Broker.DefaultQueueConfigDatas;

    }
    public class DefaultDistributorConfigModel
    {
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
    public class DefaultQueueConfigModel
    {
        public string QueueKey { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public SendTypeEnum SendType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
    public class HostConfigModel
    {
        internal string HostName => ServerConst.Hosting.HostName;
        public int Port => ServerConst.Hosting.Port;
        public int PortSsl => ServerConst.Hosting.PortSsl;
    }
    public class TimingConfigModel
    {
        internal ScheduleFactoryEnum ScheduleFactory => ServerConst.Timing.ScheduleFactory;
        internal TimingFactoryEnum TimingFactory => ServerConst.Timing.TimingFactory;
    }
    public class PublisherConfigModel
    {
        internal PublisherFactoryEnum PublisherFactory => ServerConst.Publisher.PublisherFactory;
    }
}
