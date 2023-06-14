using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventManagementApp.Extensions
{
    public static class UserManagerExtinsions
    {
        public static async Task<AppUser> FindUserByClaimsPrinciplsWithAddress(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
            
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Email == email);
        }


        public static async Task<AppUser> FindUserByClaimsPrinciplsWithEmail(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}
