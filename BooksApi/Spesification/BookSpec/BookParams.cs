namespace BooksApi.Spesification.BookSpec
{
    public class BookParams
    {

        public int? AutherID { get; set; }

        const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        int pageSize = 9;
        public int PageSize
        {
            get {return pageSize; }

            set {pageSize= value>MaxPageSize? 50:value; }
        }
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
