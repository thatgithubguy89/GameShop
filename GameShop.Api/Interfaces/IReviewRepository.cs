using GameShop.Api.Models;

namespace GameShop.Api.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetReviewsByGameIdAsync(int gameId);
    }
}
