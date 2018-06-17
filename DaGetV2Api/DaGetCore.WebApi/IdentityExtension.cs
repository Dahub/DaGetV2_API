using System;
using System.Linq;

namespace DaGetCore.WebApi
{
    internal static class IdentityExtension
    {
        // this System.Security.Claims.ClaimsIdentity
        internal static Guid? GetUserId(this System.Security.Principal.IIdentity id)
        {
            Guid? toReturn = null;
            if (id.GetType() == typeof(System.Security.Claims.ClaimsIdentity))
            {
                string myId = ((System.Security.Claims.ClaimsIdentity)id).Claims.Where(c => c.Type.Equals("user_public_id", StringComparison.OrdinalIgnoreCase)).Select(c => c.Value).FirstOrDefault();
                if(Guid.TryParse(myId, out Guid testGuid))
                {
                    toReturn = testGuid;
                }
            }
            return toReturn;
        }
    }
}
