using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace GameShop.Test.Repositories
{
    public class RepositoryTests
    {
        IRepository<Game> _repository;
        GameShopContext _context;
        DbContextOptions<GameShopContext> _contextOptions = new DbContextOptionsBuilder<GameShopContext>()
            .UseInMemoryDatabase("GameRepositoryTests")
            .Options;

        private static readonly Game _mockGame = new Game { Id = 1, Title = "test" };

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

            _repository = new Repository<Game>(_context);

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

            var result = await _repository.GetAllAsync();

            result.ShouldBeOfType<List<Game>>();
            result.Count.ShouldBe(3);
        }

        [Test]
        public async Task GetByIdAsync()
        {
            await _context.Games.AddAsync(_mockGame);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(1);

            result.ShouldBeOfType<Game>();
            result.Id.ShouldBe(1);
            result.Title.ShouldBe("test");
        }

        [Test]
        public async Task GetByIdAsync_GivenIdForGameThatDoesNotExist_ShouldReturnNull()
        {
            var result = await _repository.GetByIdAsync(1);

            result.ShouldBeNull();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetByIdAsync_GivenInvalidId_ShouldThrowArgumentOutOfRangeException(int id)
        {
            await Should.ThrowAsync<ArgumentOutOfRangeException>(async () => await _repository.GetByIdAsync(id));
        }
    }
}
