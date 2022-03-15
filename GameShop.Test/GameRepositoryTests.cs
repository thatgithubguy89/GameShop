using GameShop.Data;
using GameShop.Models;
using GameShop.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace GameShop.Test
{
    public class GameRepositoryTests
    {
        DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GameRepositoryTests")
            .Options;
        ApplicationDbContext _context;
        IGameRepository _gameRepository;

        Game game = new Game
        {
            Id = 1,
            Title = "test",
            Description = "test",
            Price = 1.1M,
            TrailerLink = "test",
            ImagePath = "test",
            CreateDate = DateTime.Now,
            LastEditDate = DateTime.Now
        };

        Game game2 = new Game
        {
            Id = 2,
            Title = "test",
            Description = "test",
            Price = 1.1M,
            TrailerLink = "test",
            ImagePath = "test",
            CreateDate = DateTime.Now,
            LastEditDate = DateTime.Now
        };

        [SetUp]
        public void Setup()
        {
            _context = new ApplicationDbContext(_contextOptions);
            _gameRepository = new GameRepository(_context);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddAsync()
        {
            await _gameRepository.AddGameAsync(game);

            var result = await _gameRepository.GetGameByIdAsync(1);
            result.Title.ShouldBe("test");
        }

        [Test]
        public async Task AddAsync_Exception()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _gameRepository.AddGameAsync(null));
        }

        [Test]
        public async Task DeleteAsync()
        {
            await _gameRepository.AddGameAsync(game);
            await _gameRepository.DeleteGameAsync(game);

            var result = await _gameRepository.GetGameByIdAsync(1);
            result.ShouldBeNull();
        }

        [Test]
        public async Task DeleteAsync_Exception()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _gameRepository.DeleteGameAsync(null));
        }

        [Test]
        public async Task GetAllAsync()
        {
            await _gameRepository.AddGameAsync(game);
            await _gameRepository.AddGameAsync(game2);

            var result = await _gameRepository.GetAllGamesAsync();
            result.Count.ShouldBe(2);
        }

        [Test]
        public async Task UpdateAsync()
        {
            await _gameRepository.AddGameAsync(game);
            game.Title = "test2";
            await _gameRepository.UpdateGameAsync(game);

            var result = await _gameRepository.GetGameByIdAsync(1);
            result.Title.ShouldBe("test2");
        }

        [Test]
        public async Task UpdateAsync_Exception()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _gameRepository.UpdateGameAsync(null));
        }
    }
}