using GameShop.Api.Models;

namespace GameShop.Api.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
