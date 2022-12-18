using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.DbOpr;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class DbOprConfigModel
    {
        internal DbOprServiceFactoryEnum DbOprServiceFactory => ServerConst.DbOpr.DbOprServiceFactory;
        internal DbOprFactoryEnum DbOprFactory => ServerConst.DbOpr.DbOprFactory;
        internal DbMemConfigModel DbMemConfig => new DbMemConfigModel();
        internal DbTextConfigModel DbTextConfig => new DbTextConfigModel();
        internal DboCreatorFactoryEnum DboCreatorFactory => ServerConst.DbOpr.DboCreatorFactory;
        public string CompletedDataRemovesTimer => ServerConst.DbOpr.CompletedDataRemovesTimer;
    }
}
