using JoberMQ.Common.Helpers;
using JoberMQ.Common.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JoberMQ.Controllers
{
    public class AccountController : ControllerBase
    {
        private JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
        //private readonly JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
        //private readonly IServerService ServerService;
        //private readonly IClientServerService ClientServerService;
        //private readonly IUserDbOperation UserDbOperation;
        //public AccountController(IServerService ServerService, IClientServerService ClientServerService, IUserDbOperation UserDbOperation)
        //{
        //    this.ServerService = ServerService;
        //    this.ClientServerService = ClientServerService;
        //    this.UserDbOperation = UserDbOperation;
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var result = new ResponseLoginModel();


            if (JoberHost.JoberMQ == null || JoberHost.IsJoberActive == false)
            {
                result.IsSuccess = false;
                result.StatusCode = "0.0.13";
                //result.Message = JoberHost.Jober.StatusCode.GetStatusMessage("0.0.13");
                result.Message = "Sunucu hazırlanıyor, erişemezsiniz.";
                return Unauthorized(JsonConvert.SerializeObject(result));
            }


            var authHeader = HttpContext.Request.Headers["Authorization"].First().Substring("Basic ".Length).Trim();
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var split = encoding.GetString(Convert.FromBase64String(authHeader)).Split(':');
            var userName = split[0];
            var password = split[1];
            var clientKey = HttpContext.Request.Headers["clientKey"].ToString();

            var clientCheck = JoberHost.JoberMQ.Clients.Get(x => x.ClientKey == clientKey);
            //var clientCheck = Startup.ClientService.ClientData.Get(x => x.ClientKey == clientKey);

            if (clientCheck != null)
            {
                result.IsSuccess = false;
                result.StatusCode = "0.0.10";
                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("0.0.10");
                return Unauthorized(JsonConvert.SerializeObject(result));
            }

            var userCheck = JoberHost.JoberMQ.Database.User.DbMem.Get(x => x.UserName == userName && x.Password == CryptionHashHelper.SHA256EnCryption(password));
            if (userCheck == null)
            {
                result.IsSuccess = false;
                result.StatusCode = "0.0.11";
                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("0.0.11");
                return Unauthorized(JsonConvert.SerializeObject(result));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JoberHost.JoberMQ.Configuration.ConfigurationSecurity.SecurityKey));
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userCheck.UserName), new Claim(ClaimTypes.Name, userCheck.UserName), new Claim(ClaimTypes.Role, userCheck.Authority) };
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, signingCredentials: credentials);

            result.IsSuccess = true;
            result.Token = jwtTokenHandler.WriteToken(token);

            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
