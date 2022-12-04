namespace BooksApi.Models
{
    public class Language
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Nationality Nationality { get; set; }
    }
}
