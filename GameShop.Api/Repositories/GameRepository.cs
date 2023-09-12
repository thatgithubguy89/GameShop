using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly GameShopContext _context;
        private readonly ICacheService<Game> _cacheService;

        public GameRepository(GameShopContext context, ICacheService<Game> cacheService) : base(context)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public override async Task<List<Game>> GetAllAsync()
        {
            var games = await _cacheService.GetItemsAsync("all-games");
            if (games != null)
                return games;

            games = await _context.Games.AsNoTracking()
                                        .OrderByDescending(g => g.ReleaseDate)
                                        .ToListAsync();

            if (games != null)
                await _cacheService.SetItemsAsync(games, "all-games");

            return games!;
        }

        public async Task<List<Game>> GetGamesByIdsAsync(List<int> ids)
        {
            ids.RemoveAll(i => i == 0);

            var games = new List<Game>();

            foreach (var id in ids)
            {
                var game = await base.GetByIdAsync(id);
                if (game != null)
                    games.Add(game);
            }

            return games!;
        }
    }
}
