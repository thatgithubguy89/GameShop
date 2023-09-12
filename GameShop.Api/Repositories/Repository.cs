using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GameShopContext _context;

        public Repository(GameShopContext context)
        {
            _context = context;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var createdEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return null;

            _context.Entry<T>(entity).State = EntityState.Detached;

            return entity;
        }
    }
}
