using BooksApi.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class FavoritList
    {
        public int Id { get; set; }

        //[ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        //public virtual ApplicationUser UserFavorite { get; set; }


        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual List<Book> BookFavourite { get; set; }=new List<Book>(){ };
    }
}