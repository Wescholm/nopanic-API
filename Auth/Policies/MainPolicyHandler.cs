using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace nopanic_API.Auth.Policies
{
    public class MainPolicyHandler: AuthorizationHandler<MainPolicyRequirement>
    {
        readonly IHttpContextAccessor _contextAccessor;

        public MainPolicyHandler(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MainPolicyRequirement requirement) {
            
            var result = await requirement.isPass(_contextAccessor, context);
            if (result)
                context.Succeed(requirement);
            else
                context.Fail();
        }
    }
}