using JoberMQ.Entities.Base.Dbo;

namespace JoberMQ.Entities.Dbos
{
    internal class UserDbo : DboPropertyGuidBase, IDboBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
