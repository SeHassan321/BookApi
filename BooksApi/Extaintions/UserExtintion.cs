using BooksApi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace BooksApi.Extaintions
{
    public static class UserExtintion
    {
        public static async Task<ApplicationUser> FindByEmailWithAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);

            var UserToView = await userManager.Users.Include(u => u.Addresses).Include(u => u.FavoritLists).ThenInclude(f=>f.BookFavourite).ThenInclude(bf=>bf.Images).SingleOrDefaultAsync(u => u.Email == email);
            return (UserToView);

        }
    }
}
