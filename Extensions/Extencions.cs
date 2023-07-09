using System.Security.Claims;

namespace AutoMarketplace.Extensions
{
    public static class Extencions
    {
        /// <summary>
        /// Returns true if the user is Admin
        /// </summary>
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");
        }
    }
}
