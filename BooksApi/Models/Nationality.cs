using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class Nationality
    {

        public int? Id { get; set; }
        public string? Name { get; set; }
        public  Auther? auther { get; set; }
        public  Language ? Language { get; set; }

        [ForeignKey("Language")]
        public int? LanguageId { get; set; }
    }
}
