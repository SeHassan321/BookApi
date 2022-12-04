using System.Linq.Expressions;

namespace BooksApi.Spesification
{
    public interface Ispesifaction<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public List<T> thenInclude { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationOn { get; set; }
    }
}
