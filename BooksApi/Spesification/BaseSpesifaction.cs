using System.Linq.Expressions;

namespace BooksApi.Spesification
{
    public class BaseSpesifaction<T> : Ispesifaction<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public List<T> thenInclude { get; set; } = new List<T>();

        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationOn { get; set; }

        public BaseSpesifaction(Expression<Func<T, bool>> criteira)
        {
            Criteria = criteira;

        }
        public BaseSpesifaction()
        {

        }
        public void addIncludes(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        public void addThenIncludes(T theninclude)
        {
            thenInclude.Add(theninclude);
        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginationOn = true;
            Skip = skip;
            Take = take;
        }
    }
}

