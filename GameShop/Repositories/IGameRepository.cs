using GameShop.Models;

namespace GameShop.Repositories
{
    public interface IGameRepository
    {
        Task AddGameAsync(Game game);
        Task DeleteGameAsync(Game game);
        Task<List<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task UpdateGameAsync(Game game);
    }
}
