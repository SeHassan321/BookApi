using BooksApi.Dto;
using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    ///////////////////////////
    public class NationalitiesController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public NationalitiesController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult GetAllNationalities()
        {
            var Nationalities = _Context.Nationalities
                .Include(n => n.Language)
                .Select(s => new
                {
                    NationalityId = s.Id,
                    Nationality = s.Name,
                    Language = s.Language.Name
                }).ToList();

            return Ok(Nationalities);
        }

        [HttpGet("{id}")]
        public IActionResult GetAllNationalitiesById(int id)
        {
            var Nationality = _Context.Nationalities.Find(id);
            if (Nationality == null)
                NotFound("Nationality Not Found");
            return Ok(Nationality);
        }
        [HttpPost]
        public IActionResult AddNationality([FromBody] NationalityDto dto)
        {
            var Nationality = new Nationality
            {
                Name = dto.Name,
                LanguageId = dto.LanguageId,

            };
            _Context.Add(Nationality);
            _Context.SaveChanges();
            return Ok(Nationality);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateNationality(int id, [FromBody] NationalityDto dto)
        {
            var Nationality = _Context.Nationalities.Find(id);
            if (Nationality == null)
                NotFound("Nationality Not Found");

            Nationality.Name = dto.Name;
            Nationality.LanguageId = dto.LanguageId;

            _Context.SaveChanges();

            return Ok(Nationality);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNationality(int id)
        {
            var Nationality = _Context.Nationalities.Find(id);
            if (Nationality == null)
                NotFound("Nationality Not Found");

            _Context.Nationalities.Remove(Nationality);

            _Context.SaveChanges();

            return Ok();
        }
    }
}
