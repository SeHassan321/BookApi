using BooksApi.Models;

namespace BooksApi.Dto
{
    public class CurrentUserDTO
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; }
        public string[]? BookImage { get; set; }

        public string Role { get; set; }
        public virtual List<UserBook>? UserBooks { get; set; } = new List<UserBook>();
        public List<FavoritList>? FavoritLists { get; set; } = new List<FavoritList>();
    }
}
