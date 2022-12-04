
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Spesification
{
    public class spesificationEvalauter<T> where T : class
    {
        public static IQueryable<T> getQuery(IQueryable<T> inputQuery, Ispesifaction<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            //context.set<T>.where(p=>p.id)

            if (spec.IsPaginationOn)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (currentQuery, index) => currentQuery.Include(index));
            //query = spec.Includes.Aggregate(query, (currentQuery, index) => currentQuery.Include(index).ThenInclude());

            return query;
            //context.set<T>.where(p=>p.id).include(f irst).include(second)
        }
    }
}
