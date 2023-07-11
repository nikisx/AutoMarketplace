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

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
