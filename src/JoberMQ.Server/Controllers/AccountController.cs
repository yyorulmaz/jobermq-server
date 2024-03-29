﻿using JoberMQ.Entities.Models.Login;
using JoberMQ.Server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Server.Controllers
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

            if (!Factory.Server.IsServerActive)
            {
                result.IsSuccess = false;
                result.StatusCode = "0.0.13";
                result.Message = Factory.Server.StatusCode.GetStatusMessage("0.0.13");
                return Unauthorized(JsonConvert.SerializeObject(result));
            }

            var authHeader = HttpContext.Request.Headers["Authorization"].First().Substring("Basic ".Length).Trim();
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var split = encoding.GetString(Convert.FromBase64String(authHeader)).Split(':');
            var userName = split[0];
            var password = split[1];
            var clientKey = HttpContext.Request.Headers["clientKey"].ToString();

            Factory.Server.ClientService.Clients.TryGetValue(clientKey, out var clientCheck);
            //var clientCheck = Startup.ClientService.ClientData.Get(x => x.ClientKey == clientKey);

            if (clientCheck != null)
            {
                result.IsSuccess = false;
                result.StatusCode = "0.0.10";
                result.Message = Factory.Server.StatusCode.GetStatusMessage("0.0.10");
                return Unauthorized(JsonConvert.SerializeObject(result));
            }

            var userCheck = Factory.Server.DbOprService.User.Check(userName, CryptionHashHelper.SHA256EnCryption(password));
            if (!userCheck)
            {
                result.IsSuccess = false;
                result.StatusCode = "0.0.11";
                result.Message = Factory.Server.StatusCode.GetStatusMessage("0.0.11");
                return Unauthorized(JsonConvert.SerializeObject(result));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Factory.Server.ServerConfig.SecurityConfig.SecurityKey));
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userName), new Claim(ClaimTypes.Name, userName) };
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, signingCredentials: credentials);

            result.IsSuccess = true;
            result.Token = jwtTokenHandler.WriteToken(token);

            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
