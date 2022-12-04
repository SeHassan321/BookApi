using BooksApi.Dto;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthersController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public AuthersController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult GetAllAuthers()
        {
            var authers = _Context.Authers
                .Include(a => a.Nationalities)
                .ThenInclude(n => n.Language)
                .Select(s => new
                {
                    AutherId = s.AutherId,
                    Auther = s.Name,
                    Nationality=s.Nationalities.Name,
                    Language = s.Nationalities.Language.Name
                }).ToList();

            return Ok(authers);
        }

        [HttpGet("{id}")]
        public IActionResult GetAllAuthers(int id)
        {
            var auther = _Context.Authers.Find(id);
            return Ok(auther);
        }
        [HttpPost]
        public IActionResult AddAuther([FromBody] AutherDto dto)
        {
            var auther = new Auther
            {
                Name = dto.Name,
                NationalityId = dto.NationalityId,

            };
            _Context.Add(auther);
            _Context.SaveChanges();
            return Ok(auther);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuther(int id, [FromBody] AutherDto dto)
        {
            var auther = _Context.Authers.Find(id);
            if (auther == null)
                NotFound("Auther Not Found");

            auther.Name = dto.Name;
            auther.NationalityId = dto.NationalityId;

            _Context.SaveChanges();

            return Ok(auther);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuther(int id)
        {
            var auther = _Context.Authers.Find(id);
            if (auther == null)
                NotFound("Auther Not Found");

            _Context.Authers.Remove(auther);

            _Context.SaveChanges();

            return Ok();
        }

    }
}