﻿using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.DataAccess.Repository.DbText.Implementation;
using JoberMQ.Entities.Dbos;
using JoberMQ.Entities.Models.Config;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfJobDataTextDal : DfDbTextRepository<JobDataDbo>, IJobDataTextDal
    {
        public DfJobDataTextDal(DbTextFileConfigModel dbTextFileConfig) : base(dbTextFileConfig)
        {
        }
    }
}
