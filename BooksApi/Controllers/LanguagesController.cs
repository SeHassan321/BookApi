using BooksApi.Dto;
using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public LanguagesController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult GetAllLanguages()
        {
            var Languages = _Context.Languages
                .Select(s => new
                {
                    LanguageId = s.Id,
                    Language = s.Name
                }).ToList();

            return Ok(Languages);
        }

        [HttpGet("{id}")]
        public IActionResult GetAllLanguagesById(int id)
        {
            var Language = _Context.Languages.Find(id);
            if (Language == null)
                NotFound("Language Not Found");
            return Ok(Language);
        }
        [HttpPost]
        public IActionResult AddLanguage([FromBody] LanguageDto dto)
        {
            var Language = new Language
            {
                Name = dto.Name,

            };
            _Context.Add(Language);
            _Context.SaveChanges();
            return Ok(Language);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, [FromBody] LanguageDto dto)
        {
            var Language = _Context.Languages.Find(id);
            if (Language == null)
                NotFound("Language Not Found");

            Language.Name = dto.Name;

            _Context.SaveChanges();

            return Ok(Language);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            var Language = _Context.Languages.Find(id);
            if (Language == null)
                NotFound("Language Not Found");

            _Context.Languages.Remove(Language);

            _Context.SaveChanges();

            return Ok();
        }


    }
}
