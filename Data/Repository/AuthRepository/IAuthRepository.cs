using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using nopanic_API.Models;

namespace nopanic_API.Data.Repository.AuthRepository
{
    public interface IAuthRepository
    {
        bool Register(User user);
        Dictionary<string, object> Login(User user);
        void Logout(string token);
        Dictionary<string, string> RefreshToken(string token);
        bool IsTokenValid(string token);
        bool CheckUserAvailability(string data, string type);
    }
}