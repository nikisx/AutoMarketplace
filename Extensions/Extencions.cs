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
        
        /// <summary>
        /// Returns the curren user Id
        /// </summary>
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
