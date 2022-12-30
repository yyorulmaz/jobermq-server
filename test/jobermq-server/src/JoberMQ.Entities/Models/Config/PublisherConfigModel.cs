using JoberMQ.Entities.Constants;
using JoberMQ.Entities.Enums.Publisher;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Entities.Models.Config
{
    public class PublisherConfigModel
    {
        internal PublisherFactoryEnum PublisherFactory => ServerConst.Publisher.PublisherFactory;
    }
}
