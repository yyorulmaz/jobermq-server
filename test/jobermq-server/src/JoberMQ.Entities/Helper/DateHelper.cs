using System;

namespace JoberMQ.Entities.Helper
{
    public class DateHelper
    {
        public static DateTime GetUniversalNow() => DateTime.Now.ToUniversalTime();
    }
}
