using BooksApi.InterFaces;
using BooksApi.Models;
using BooksApi.Spesification;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repositories
{
    public class GenercRepositry<T> : IGenercRepositry<T> where T : class
    {
        private readonly ApplicationDbContext context;
        public GenercRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(T entity)
                  => await context.Set<T>().AddAsync(entity);

        public T Delete(T entity)
            => context.Set<T>().Remove(entity).Entity;

        public async Task<IReadOnlyList<T>> GetAllDataAsync()

            => await context.Set<T>().ToListAsync();

        public async Task<T> GetDataByIdAsync(int id)

            => await context.Set<T>().FindAsync(id);

        public void SaveChange()
        {
            context.SaveChanges();
        }

        public T Update(T entity)
            => context.Set<T>().Update(entity).Entity;


        public async Task<IReadOnlyList<T>> GetAllDataWithSpecAsync(Ispesifaction<T> spec)
        {
            return await ApplySpesifacation(spec).ToListAsync();
        }

        public async Task<T> GetDataByIdWithSpecAsync(Ispesifaction<T> spec)
        {
            return await ApplySpesifacation(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(Ispesifaction<T> spec)
        {
            return await ApplySpesifacation(spec).CountAsync();
        }


        ////////  *   applly specifications Place *  /////////////////
        private IQueryable<T> ApplySpesifacation(Ispesifaction<T> spec)
        {
            return spesificationEvalauter<T>.getQuery(context.Set<T>(), spec);
        }


    }
}
