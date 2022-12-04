using BooksApi.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class UserBook
    {
        public int Id { get; set; }

        //[ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        //public virtual ApplicationUser ApplicationUsers { get; set; }


        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Books { get; set; }
    }
}
