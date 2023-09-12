using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace GameShop.Test.Repositories
{
    public class ReviewRepositoryTests
    {
        IReviewRepository _reviewRepository;
        Mock<ICacheService<Review>> _mockCacheService;
        GameShopContext _context;
        DbContextOptions<GameShopContext> _contextOptions = new DbContextOptionsBuilder<GameShopContext>()
            .UseInMemoryDatabase("GameRepositoryTests")
            .Options;

        private static readonly Game _mockGame = new Game
        {
            Id = 1,
            Title = "test"
        };

        private static readonly List<Review> _mockReviews = new List<Review>
        {
            new Review {Id = 1, Title = "test1", GameId = 1},
            new Review {Id = 2, Title = "test1", GameId = 1},
            new Review {Id = 3, Title = "test1", GameId = 1}
        };

        [SetUp]
        public void Setup()
        {
            _context = new GameShopContext(_contextOptions);

            _mockCacheService = new Mock<ICacheService<Review>>();

            _reviewRepository = new ReviewRepository(_context, _mockCacheService.Object);

            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetReviewsByGameIdAsync()
        {
            await _context.Games.AddAsync(_mockGame);
            await _context.Reviews.AddRangeAsync(_mockReviews);
            await _context.SaveChangesAsync();

            var result = await _reviewRepository.GetReviewsByGameIdAsync(_mockGame.Id);

            result.Count.ShouldBe(3);
            result.ShouldBeOfType<List<Review>>();
        }

        [Test]
        public async Task GetReviewsByGameIdAsync_CacheFound()
        {
            _mockCacheService.Setup(c => c.GetItemsAsync(It.IsAny<string>())).Returns(Task.FromResult(_mockReviews));

            var result = await _reviewRepository.GetReviewsByGameIdAsync(_mockGame.Id);

            result.Count.ShouldBe(3);
            result.ShouldBeOfType<List<Review>>();
        }
    }
}
