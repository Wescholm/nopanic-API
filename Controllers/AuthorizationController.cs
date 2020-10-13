using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nopanic_API.Data.Repository.AuthRepository;
using nopanic_API.Data.Repository.AWSAuthRepository;
using nopanic_API.Models;

namespace nopanic_API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private IAuthRepository _repository = new AuthRepository();
        private IAWSAuthRepository _AWS_repository = new AWSAuthRepository();

        [HttpGet("GetAuthorizationHeader")]
        public object GenerateAuthorizationHeader()
        {
            var header = _AWS_repository
                .GenerateAuthorizationHeader("img.nopanic", "file_name.png", HttpMethod.Get);
            return header;
        }

        
        [HttpGet("GetUserAvailability")]
        public IActionResult CheckUserAvailability([FromQuery] string data, string type)
        {
            var isAvailability = _repository.CheckUserAvailability(data, type);
            return Ok(isAvailability);
        }
        
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append(
                "rt",
                "",
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(-1)
                });
            
            _repository.Logout(Request.Cookies["rt"]);
            return NoContent();
        }
        
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["rt"];
            if (refreshToken == null) return Unauthorized("Refresh token cannot be null");
            
            var tokens = _repository.RefreshToken(refreshToken);
            if (tokens == null) return Unauthorized("Refresh token is not valid");

            DateTime now = DateTime.Now;
            HttpContext.Response.Cookies.Append(
                "rt",
                tokens["rt"],
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = now.AddMonths(2)
                });
            
            var response = new Dictionary<string, string>()
            {
                {"jwt", tokens["jwt"]}
            };
            
            return Ok(response);
        }
        
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            var userInfo = _repository.Login(user);
            if (userInfo == null) return Unauthorized("Wrong login or password");

            DateTime now = DateTime.Now;
            HttpContext.Response.Cookies.Append(
                "rt",
                userInfo["rt"].ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = now.AddMonths(2)
                });

            userInfo.Remove("rt");
            return Ok(userInfo);
        }
        
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user.Email == null || user.Password == null || user.UserName == null)
            {
                return BadRequest("Email, username or password cannot be empty");
            } 
            
            var isRegister = _repository.Register(user);
            if (!isRegister) return Conflict("User is already registered");
            
            return Ok();
        }
    }
}