﻿using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Database.Abstraction.Repository.DbMem;
using JoberMQ.Database.Abstraction.Repository.DbText;
using JoberMQ.Database.Implementation.Repository.DbOpr.Default;
using JoberMQ.Entities.Dbos;
using JoberMQ.Server.Factories.DbOpr;
using System;
namespace JoberMQ.Database.Implementation.DbOpr.Default
{
    internal class DfEventSubDbOpr : DfDbOprRepository<EventSubDbo>, IEventSubDbOpr
    {
        internal DfEventSubDbOpr(IConfigurationDatabase configuration)
        {
            this.DbMem = DbMemFactory.CreateDbMemEventSub(configuration.DbMemFactory, configuration.DbMemDataFactory);
            this.DbText = DbTextFactory.CreateDbTextEventSub(configuration);
        }

        internal DfEventSubDbOpr(IDbMemRepository<Guid, EventSubDbo> dbMem, IDbTextRepository<EventSubDbo> dbText) : base(dbMem, dbText)
        {
        }
    }
}
