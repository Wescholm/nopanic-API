using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using nopanic_API.Managers;

namespace nopanic_API.Auth.Policies
{
    public class MainPolicyRequirement: IAuthorizationRequirement
    {

        public async Task<bool> isPass(IHttpContextAccessor contextAccessor, AuthorizationHandlerContext authorizationHandlerContext)
        {
            try
            {
                var authToken = contextAccessor.HttpContext.Request.Headers["Authorization"];
                var JWTService = new JWTService("TW9zaGVFcmV6UHJpdmF0ZUtleQ==");
                return JWTService.IsTokenValid(authToken);
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}