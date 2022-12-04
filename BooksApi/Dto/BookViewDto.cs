using BooksApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dto
{
    public class BookViewDto
    {

 
        public string Title { get; set; }
        public string? Overview { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string AutherName { get; set; }
        public string[] Images { get; set; } 
        public string language { get; set; } 
    }
}
