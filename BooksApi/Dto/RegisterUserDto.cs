using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dto
{
    public class RegisterUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumper { get; set; }
        public string? Password { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public IFormFile? imgName { get; set; }
    }
}
