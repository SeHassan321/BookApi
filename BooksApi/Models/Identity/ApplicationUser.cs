using Microsoft.AspNetCore.Identity;

namespace BooksApi.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Address? Addresses { get; set; }
        public string? ImageName { get; set; }
        public virtual List<UserBook>? UserBooks { get; set; } =new List<UserBook>();
        public virtual List<FavoritList>? FavoritLists { get; set; } = new List<FavoritList>();
    }
}
