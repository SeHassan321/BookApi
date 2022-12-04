using BooksApi.Spesification;

namespace BooksApi.InterFaces
{
    public interface IGenercRepositry<T> where T : class
    {
        Task Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<IReadOnlyList<T>> GetAllDataAsync();
        Task<IReadOnlyList<T>> GetAllDataWithSpecAsync(Ispesifaction<T> spec);
        Task<T> GetDataByIdWithSpecAsync(Ispesifaction<T> spec);
        Task<T> GetDataByIdAsync(int id);
        Task<int> GetCountAsync(Ispesifaction<T> spec);
        void SaveChange();
    }
}
