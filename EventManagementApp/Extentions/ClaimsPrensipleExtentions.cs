using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace EventManagementApp.Extensions
{
    public static class ClaimsPrensipleExtentions
    {
        public static string ReteriveEmailPrenciples(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
