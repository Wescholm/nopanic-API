using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace nopanic_API.Models
{
    public class JWTContainerModel: IAuthContainerModel
    {
        #region Public Methods

        public string SecretKey { get; set; } = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 30;

        public Claim[] Claims { get; set; }

        #endregion
    }
}