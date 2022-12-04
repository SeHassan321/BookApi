using BooksApi.interFaces;
using BooksApi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksApi.Repositories
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateToken(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var authClime = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.GivenName , user.UserName)
            };
            var authRole = await userManager.GetRolesAsync(user);
            foreach (var item in authRole)
            {
                authClime.Add(new Claim(ClaimTypes.Role, item.ToString()));
            };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: authClime,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}

