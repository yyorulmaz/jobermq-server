namespace JoberMQ.Server.Abstraction.Config
{
    public interface ISecurityConfig
    {
        internal string SecurityKey { get; }
        public bool SetSecurityConfig(string securityKey);
    }
}
