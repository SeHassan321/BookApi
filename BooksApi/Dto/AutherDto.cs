using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Dto
{
    public class AutherDto
    {

        public string Name { get; set; }
        public int? NationalityId { get; set; }
    }
}
