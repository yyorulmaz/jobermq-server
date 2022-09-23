using JoberMQ.Entities.Base.Model;
using System;

namespace JoberMQ.Entities.Models.Response
{
    public class JobDataAddResponseModel : ResponseBaseModel
    {
        public Guid? JobId { get; set; }
    }
}
