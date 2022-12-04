using BooksApi.Models;

namespace BooksApi.Spesification.BookSpec
{
    public class BookSpec : BaseSpesifaction<Book>
    {
        public BookSpec(BookParams bookParams) : base(
            b => (!bookParams.AutherID.HasValue || b.AutherId == bookParams.AutherID)
            &&(string.IsNullOrEmpty(bookParams.Search)||b.Title.Contains(bookParams.Search))) //احنا كده باعتين كذا كريتريا واللي تطلع ب ترو ياخدها
        
        {
            addIncludes(b => b.Auther);
            addIncludes(b => b.Images);
            //addThenIncludes(Book);

            ApplyPagination(bookParams.PageSize * (bookParams.PageIndex - 1), bookParams.PageSize);
        }
        public BookSpec(int id) : base(b => b.Id == id)
        {
            addIncludes(b => b.Auther);
            addIncludes(b => b.Images);
        }

    }
}
