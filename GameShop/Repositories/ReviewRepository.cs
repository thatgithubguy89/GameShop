using GameShop.Data;
using GameShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Repositories
{
    [Authorize]
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(Review review)
        {
            if (review == null)
                throw new ArgumentNullException();

            review.CreateDate = DateTime.Now;

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Review review)
        {
            if (review == null)
                throw new ArgumentNullException();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public Task<List<Review>> GetAllReviewsAsync()
        {
            return _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            if (review == null)
                throw new ArgumentNullException();

            review.LastEditDate = DateTime.Now;

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
