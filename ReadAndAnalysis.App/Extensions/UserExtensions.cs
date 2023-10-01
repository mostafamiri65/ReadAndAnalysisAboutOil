using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Extensions
{
    public static class UserExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var identifier = claimsPrincipal.Claims.SingleOrDefault(s =>
            s.Type == ClaimTypes.NameIdentifier);

            if (identifier == null) return 0;

            return Convert.ToInt64(identifier.Value);
        }
    }
}
