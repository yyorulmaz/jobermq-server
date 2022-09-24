using JoberMQ.Entities.Dbos;
using System;
using System.Collections.Generic;

namespace JoberMQ.Server.Abstraction.DboCreator
{
    internal interface IDboCreator
    {
        public JobDbo JobDboCreate(JobDataDbo jobDataDbo);
        public MessageDbo MessageDboCreate(JobDataDbo jobDataDbo, JobDataDetailDbo jobDataDetailDbo, JobDbo jobDbo, JobDetailDbo jobDetailDbo, Guid? eventGroupId);
        public List<JobDbo> CloneJobDataToJobs(JobDataDbo jobData);

    }
}
