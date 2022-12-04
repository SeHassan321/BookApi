using BooksApi.Errors;
using BooksApi.Helpers;
using BooksApi.interFaces;
using BooksApi.InterFaces;
using BooksApi.Models;
using BooksApi.Models.Identity;
using BooksApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var ConectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(Options =>
Options.UseSqlServer(ConectionString));

builder.Services.AddControllers();




builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{

}).AddEntityFrameworkStores<ApplicationDbContext>().AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

//////////////////////////////token service///////////////////////
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidateIssuer = false,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateLifetime = true,
    };
});
///////////////////////////////////////////////////////////////////





builder.Services.AddScoped(typeof(IGenercRepositry<>), typeof(GenercRepositry<>));
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

//for validation error msg (sending string innstead of int)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Where(m => m.Value.Errors.Count > 0) //untill here he will return All modelState with erros array in it
                                            .SelectMany(m => m.Value.Errors)
                                            .Select(e => e.ErrorMessage).ToList();
        var responseMessage = new ApiValidationErrorRespose()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(responseMessage);
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
