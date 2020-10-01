using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using nopanic_API.Managers;
using nopanic_API.Models;
using nopanic_API.Models.Context;

namespace nopanic_API.Data.Repository.AuthRepository
{
    public class AuthRepository: IAuthRepository
    {
        MainDbContext db = new MainDbContext();

        public void Logout(string token)
        {
            User user = db.User.FirstOrDefault(v => v.RefreshToken == token);
            if (user != null)
            {
                user.RefreshToken = null;
                db.SaveChanges();
            }
        }
        
        public bool CheckUserAvailability(string data, string type)
        {
            User user = type.ToLower() == "username"
                ? db.User.FirstOrDefault(v => v.UserName == data)
                : db.User.FirstOrDefault(v => v.Email == data);
            return user == null;
        }
        
        public Dictionary<string, string> RefreshToken(string token)
        {
            User user = db.User.FirstOrDefault(v => v.RefreshToken == token);
            if (user == null) return null;

            var jwt = GenerateJwtToken(user);
            var refreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = refreshToken;
            db.SaveChanges();
            
            var tokens = new Dictionary<string, string>
            {
                { "jwt", jwt },
                { "rt", refreshToken }
            };
            
            return tokens;
        }
        
        public Dictionary<string, object> Login(User requestedUser)
        {
            User user = db.User
                .Include(v => v.ProfileGradient)
                .FirstOrDefault(v => v.Email == requestedUser.Email && v.Password == requestedUser.Password);

            if (user == null)
            {
                user = db.User
                    .Include(v => v.ProfileGradient)
                    .FirstOrDefault(v => v.UserName == requestedUser.Email && v.Password == requestedUser.Password);
            }
            
            if (user == null) return null;

            var jwt = GenerateJwtToken(user);
            var refreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = refreshToken;
            db.SaveChanges();

            var userInfo = new Dictionary<string, object>()
            {
                {"rt", refreshToken},
                {"jwt", jwt},
                {"profileGradient", user.ProfileGradient}
            };
            
            return userInfo;
        }
        
        public bool Register(User user)
        {
            if (!CheckUserAvailability(user.Email, "Email")) return false;
            
            var r = new Random();
            var pg = db.ProfileGradients;
            var rId = r.Next(pg.Min(v => v.Id), pg.Max(v => v.Id));
            
            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                UserName =  user.UserName,
                ProfileGradientId = rId,
                Guid = Guid.NewGuid().ToString()
            };

            db.Add(newUser);
            db.SaveChanges();

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            IAuthContainerModel model = GetJWTContainerModel(user);
            IAuthService authService = new JWTService(model.SecretKey);
            
            string token = authService.GenerateToken(model);

            if (!authService.IsTokenValid(token))
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                return token;
            }
        }

        public bool IsTokenValid(string token)
        {
            return true;
            
            // IAuthContainerModel model = GetJWTContainerModel("");
            // IAuthService authService = new JWTService(model.SecretKey);
            //
            // return authService.IsTokenValid(token);
        }
            
        #region Private Methods
        private static JWTContainerModel GetJWTContainerModel(User user)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("mobile_phone", !String.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : ""), 
                    new Claim("user_guid", user.Guid)
                }
            };
        }
        #endregion
    }
}