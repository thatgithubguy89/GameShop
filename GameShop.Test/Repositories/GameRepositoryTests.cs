using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace GameShop.Test.Repositories
{
    public class GameRepositoryTests
    {
        IGameRepository _gameRepository;
        Mock<ICacheService<Game>> _mockCacheService;
        GameShopContext _context;
        DbContextOptions<GameShopContext> _contextOptions = new DbContextOptionsBuilder<GameShopContext>()
            .UseInMemoryDatabase("GameRepositoryTests")
            .Options;

        private static readonly List<Game> _mockGames = new List<Game>
        {
            new Game { Id = 1, Title = "test" },
            new Game { Id = 2, Title = "test" },
            new Game { Id = 3, Title = "test" }
        };

        [SetUp]
        public void Setup()
        {
            _context = new GameShopContext(_contextOptions);

            _mockCacheService = new Mock<ICacheService<Game>>();
            _mockCacheService.Setup(c => c.GetItemsAsync(It.IsAny<string>()));
            _mockCacheService.Setup(c => c.SetItemsAsync(It.IsAny<List<Game>>(), It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()));

            _gameRepository = new GameRepository(_context, _mockCacheService.Object);

            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetAllAsync()
        {
            await _context.Games.AddRangeAsync(_mockGames);
            await _context.SaveChangesAsync();

            var result = await _gameRepository.GetAllAsync();

            result.ShouldBeOfType<List<Game>>();
            result.Count.ShouldBe(3);
        }

        [Test]
        public async Task GetAllAsync_CacheFound()
        {
            _mockCacheService.Setup(c => c.GetItemsAsync(It.IsAny<string>())).Returns(Task.FromResult(_mockGames));

            var result = await _gameRepository.GetAllAsync();

            result.ShouldBeOfType<List<Game>>();
            result.Count.ShouldBe(3);
        }

        [Test]
        public async Task GetGamesByIdsAsync()
        {
            await _context.Games.AddRangeAsync(_mockGames);
            await _context.SaveChangesAsync();

            var result = await _gameRepository.GetGamesByIdsAsync(new List<int> { 1, 2, 3 });

            result.ShouldBeOfType<List<Game>>();
            result.Count.ShouldBe(3);
        }
    }
}