using JoberMQ.Library.Database.Base;

namespace JoberMQ.Common.Dbos
{
    public class UserDbo : DboPropertyGuidBase, IDboBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
