using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.DbOpr;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class DbMemConfigModel
    {
        internal DbMemFactoryEnum DbMemFactory => ServerConst.DbOpr.DbMemFactory;
        internal DbMemDataFactoryEnum DbMemDataFactory => ServerConst.DbOpr.DbMemDataFactory;
    }
}
