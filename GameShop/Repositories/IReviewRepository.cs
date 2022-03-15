using GameShop.Models;

namespace GameShop.Repositories
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task UpdateReviewAsync(Review review);
    }
}
