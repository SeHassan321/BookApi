using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Overview { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishedAt { get; set; }
        public virtual Auther? Auther { get; set; }

        [ForeignKey("Auther")]
        public int? AutherId { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public int? FavoritListId { get; set; }
    }
}
