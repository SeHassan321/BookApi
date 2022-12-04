using BooksApi.Models.Identity;
using Microsoft.AspNetCore.Identity;


namespace BooksApi.interFaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user,UserManager<ApplicationUser> userManager);
    }
}
