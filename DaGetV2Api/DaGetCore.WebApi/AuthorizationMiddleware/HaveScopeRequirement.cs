using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using System.Collections.Generic;

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
            try
            {
                if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
                {
                    context.Fail();
                    return Task.FromResult(0);
                }

                var claims = ((System.Security.Claims.ClaimsIdentity)context.User.Identity).Claims.Where(c => c.Type.Equals("scope", StringComparison.OrdinalIgnoreCase)).ToList();
                
                if(claims == null || claims.Count == 0)
                {
                    context.Fail();
                    return Task.FromResult(0);
                }

                IList<string> claimScopes = claims.Select(c => c.Value).ToList();
                    
                foreach(var s in Scopes)
                {
                    if(!claimScopes.Contains(s, StringComparer.OrdinalIgnoreCase))
                    {
                        context.Fail();
                        return Task.FromResult(0);
                    }
                }

                context.Succeed(requirement);
            }
            catch
            {
                context.Fail();
            }

            return Task.FromResult(0);
        }
    }
}
