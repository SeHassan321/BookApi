using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class Auther
    {
        [Column("AutherId")]
        public int AutherId { get; set; }
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }
        public  Nationality? Nationalities { get; set; }

        [ForeignKey("Nationality")]
        public int? NationalityId { get; set; }
    }
}
