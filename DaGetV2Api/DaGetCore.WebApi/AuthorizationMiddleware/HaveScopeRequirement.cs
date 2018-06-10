using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DaGetCore.WebApi
{
    public class HaveScopeRequirement : AuthorizationHandler<HaveScopeRequirement>, IAuthorizationRequirement
    {
        public string[] Scopes { get; private set; }

        public HaveScopeRequirement(params string[] scopes)
        {
            Scopes = scopes;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HaveScopeRequirement requirement)
        {
            if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
                context.Fail();
            else
                // claim = scope
                //context.User.Identity.
                context.Succeed(requirement);

            return Task.FromResult(0);
        }
    }
}
