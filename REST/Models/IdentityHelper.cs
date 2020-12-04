using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace REST.Models
{
    public static class IdentityHelper
    {
        public static string GetFirstName(this IIdentity identity)
        {
            var claimIdent = identity as ClaimsIdentity;
            return claimIdent != null &&
                claimIdent.HasClaim(c => c.Type == "FirstName") ? claimIdent.FindFirst("FirstName").Value : string.Empty;
        }
    }
}