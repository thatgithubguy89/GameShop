using GameShop.Data;
using GameShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGameAsync(Game game)
        {
            if (game == null)
                throw new ArgumentNullException();

            game.CreateDate = DateTime.Now;

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(Game game)
        {
            if (game == null)
                throw new ArgumentNullException();

            var reviews = _context.Reviews.Where(r => r.GameId == game.Id);

            _context.RemoveRange(reviews);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            return await _context.Games
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateGameAsync(Game game)
        {
            if (game == null)
                throw new ArgumentNullException();

            game.LastEditDate = DateTime.Now;

            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
