using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public Book Books { get; set; }

        //[ForeignKey("Book")]
        public int BookId { get; set; }
    }
}
