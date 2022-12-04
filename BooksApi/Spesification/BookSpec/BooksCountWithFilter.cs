using BooksApi.Models;

namespace BooksApi.Spesification.BookSpec
{
    public class BooksCountWithFilter:BaseSpesifaction<Book>
    {
        public BooksCountWithFilter(BookParams bookParams) : base(b => !bookParams.AutherID.HasValue || b.AutherId == bookParams.AutherID) 
        { 

        } //احنا كده باعتين 2 كريتريا واللي تطلع ب ترو ياخدها

    }
}
