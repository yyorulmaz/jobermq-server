﻿using JoberMQ.Data.Implementation.Repository.DbText.Default;
using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfEventSubTextDal : DfDbTextRepository<EventSubDbo>, IEventSubTextDal
    {
        public DfEventSubTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
