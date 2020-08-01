using System.Collections.Generic;
using System.Security.Claims;
using nopanic_API.Models;

namespace nopanic_API.Managers
{
    public interface IAuthService
    {
        string SecretKey { get; set; }
        bool IsTokenValid(string token);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}