using BooksApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Dto
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public int? AutherId { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishedAt { get; set; }
        public IFormFile[]? ImagesFile { get; set; }
        public List<Image>? Images { get; set; } = new List<Image>();



    }
}

