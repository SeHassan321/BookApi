using AutoMapper;
using BooksApi.Dto;
using BooksApi.Extaintions;
using BooksApi.Helpers;
using BooksApi.interFaces;
using BooksApi.Models;
using BooksApi.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Roya.Errors;
using System.Security.Claims;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly ITokenService _Token;
        private readonly IMapper _mapper;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, ITokenService token, IMapper mapper)
        {
            _Context = context;
            _UserManager = userManager;
            _RoleManager = roleManager;
            _SignInManager = signInManager;
            _Token = token;
            _mapper = mapper;
        }


        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> emailExist([FromQuery] string email)
        {
            return await _UserManager.FindByEmailAsync(email) != null;

        }

        [HttpPost("Admin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterUserDto dto)
        {

            if (emailExist(dto.Email).Result.Value)
            {

                return BadRequest(new ApiErroeResponse(400, "this Email is Already in use ! "));
            }
            ApplicationUser addUserAdmin = new ApplicationUser();

            addUserAdmin.UserName = dto.Name;
            addUserAdmin.Email = dto.Email;

            addUserAdmin.Addresses = new Address
            {
                City = dto.City,
                Country = dto.Country,
            };
            addUserAdmin.PhoneNumber = dto.PhoneNumper;
            addUserAdmin.ImageName = DocumentSitting.addFile(dto.imgName, "Images");


            IdentityResult result = await _UserManager.CreateAsync(addUserAdmin, dto.Password);
            if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400));

            if (!await _RoleManager.RoleExistsAsync(RoleContentHelper.Admin))
                await _RoleManager.CreateAsync(new IdentityRole(RoleContentHelper.Admin));
            await _UserManager.AddToRoleAsync(addUserAdmin, RoleContentHelper.Admin);

            return Ok(dto);
        }


        [HttpPost("AddUserSeller")]
        public async Task<IActionResult> RegisterSeller([FromForm] RegisterUserDto dto)
        {

            if (emailExist(dto.Email).Result.Value)
            {

                return BadRequest(new ApiErroeResponse(400, "this Email is Already in use ! "));
            }
            ApplicationUser addUserSeller = new ApplicationUser();
            addUserSeller.UserName = dto.Name;
            addUserSeller.Email = dto.Email;

            addUserSeller.Addresses = new Address
            {
                City = dto.City,
                Country = dto.Country,
            };
            addUserSeller.PhoneNumber = dto.PhoneNumper;
            addUserSeller.ImageName = DocumentSitting.addFile(dto.imgName, "Images");

            IdentityResult result = await _UserManager.CreateAsync(addUserSeller, dto.Password);
            if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400));

            if (!await _RoleManager.RoleExistsAsync(RoleContentHelper.UserSeller))
                await _RoleManager.CreateAsync(new IdentityRole(RoleContentHelper.UserSeller));
            await _UserManager.AddToRoleAsync(addUserSeller, RoleContentHelper.UserSeller);

            return Ok(dto);
        }


        [HttpPost("AddUserBuyer")]
        public async Task<IActionResult> RegisterBuyer([FromForm] RegisterUserDto dto)
        {

            if (emailExist(dto.Email).Result.Value)
            {

                return BadRequest(new ApiErroeResponse(400, "this Email is Already in use ! "));
            }
            ApplicationUser addUserBuyer = new ApplicationUser();
            addUserBuyer.UserName = dto.Name;
            addUserBuyer.Email = dto.Email;

            addUserBuyer.Addresses = new Address
            {
                City = dto.City,
                Country = dto.Country,
            };
            addUserBuyer.PhoneNumber = dto.PhoneNumper;
            addUserBuyer.ImageName = DocumentSitting.addFile(dto.imgName, "Images");

            IdentityResult result = await _UserManager.CreateAsync(addUserBuyer, dto.Password);
            if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400));

            if (!await _RoleManager.RoleExistsAsync(RoleContentHelper.UserBuyer))
                await _RoleManager.CreateAsync(new IdentityRole(RoleContentHelper.UserBuyer));
            await _UserManager.AddToRoleAsync(addUserBuyer, RoleContentHelper.UserBuyer);

            return Ok(dto);
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginDTO>> LoginUser([FromQuery] LoginDTO loginDTO)
        {
            var user = await _UserManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiErroeResponse(400, "Email Not Found,Please Enter A Valid Email"));
            var password = await _UserManager.CheckPasswordAsync(user, loginDTO.Password);
            if (password)
            {
                var result = await _SignInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
                if (!result.Succeeded) return BadRequest(new ApiErroeResponse(400, "signIn Filed"));
            }
            else { return BadRequest(new ApiErroeResponse(400, " invalid Password")); };
            var userRole = await _UserManager.GetRolesAsync(user);
            var authUser = new UserDTO()
            {
                UserName = user.UserName,
                Roles = userRole[0],
                Token = await _Token.CreateToken(user, _UserManager),
                UserId = user.Id

            };

            return Ok(authUser);
        }





        [HttpGet]
        public async Task<ActionResult<CurrentUserDTO>> CurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _UserManager.FindByEmailWithAddressAsync(User);
            var role = await _UserManager.GetRolesAsync(user);
            if (role[0] == "Admin")
            {
                return Ok
              (new CurrentUserDTO()
              {
                  Email = email,
                  Role = role[0],
                  Name = user.UserName,
                  Phone = user.PhoneNumber,
                  City = user.Addresses.City,
                  Country = user.Addresses.Country,
                  Image = user.ImageName

              }
              );
            }
            if (role[0] == "UserBuyer")
            {
              //  return Ok
              //(new CurrentUserDTO()
              //{
              //    Email = email,
              //    Role = role[0],
              //    Name = user.UserName,
              //    Phone = user.PhoneNumber,
              //    City = user.Addresses.City,
              //    UserBooks = user.UserBooks,
              //    Country = user.Addresses.Country,
              //    FavoritLists = user.FavoritLists,
              //    Image = user.ImageName
              //}
              //);
              return _mapper.Map<ApplicationUser,CurrentUserDTO>(user);
            }

            if (role[0] == "UserSeller")
            {
                //  return Ok
                //(new CurrentUserDTO()
                //{
                //    Email = email,
                //    Role = role[0],
                //    Name = user.UserName,
                //    Phone = user.PhoneNumber,
                //    City = user.Addresses.City,
                //    UserBooks = user.UserBooks,
                //    Country = user.Addresses.Country,
                //    //FavoritLists = (List<FavoritList>)user.FavoritLists.Select(f=>f.BookFavourite),
                //    Image = user.ImageName
                //}
                //);
                return _mapper.Map<ApplicationUser, CurrentUserDTO>(user);
            }
            return Ok("you have to login");

        }


    }
}
