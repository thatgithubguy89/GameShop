using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using GameShop.Api.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shouldly;

namespace GameShop.Test.Services
{
    public class CacheServiceTests
    {
        ICacheService<Game> _cacheService;
        IDistributedCache _distributedCache;

        private static readonly List<Game> _mockGames = new List<Game>
        {
            new Game { Id = 1, Title = "test" },
            new Game { Id = 2, Title = "test" },
            new Game { Id = 3, Title = "test" }
        };

        [SetUp]
        public void Setup()
        {
            var options = Options.Create(new MemoryDistributedCacheOptions());
            _distributedCache = new MemoryDistributedCache(options);

            _cacheService = new CacheService<Game>(_distributedCache);
        }

        [TearDown]
        public void Teardown()
        {
            _cacheService.DeleteItemsAsync("games");
        }

        [Test]
        public async Task DeleteItemsAsync()
        {
            await _cacheService.SetItemsAsync(_mockGames, "games");
            var games = await _cacheService.GetItemsAsync("games");
            games.Count.ShouldBe(3);
            games.ShouldBeOfType<List<Game>>();

            await _cacheService.DeleteItemsAsync("games");
            var result = await _cacheService.GetItemsAsync("games");

            result.ShouldBeNull();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task DeleteItemsAsync_GivenInvalidKey_ShouldThrowArgumentNullException(string key)
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _cacheService.DeleteItemsAsync(key));
        }

        [Test]
        public async Task GetItemsAsync()
        {
            await _cacheService.SetItemsAsync(_mockGames, "games");

            var result = await _cacheService.GetItemsAsync("games");

            result.Count.ShouldBe(3);
            result.ShouldBeOfType<List<Game>>();
        }

        [Test]
        public async Task GetItemsAsync_GivenKeyThatDoesNotExist_ShouldBeNull()
        {
            var result = await _cacheService.GetItemsAsync("games");

            result.ShouldBeNull();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task GetItemsAsync_GivenInvalidKey_ShouldThrowArgumentNullException(string key)
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _cacheService.GetItemsAsync(key));
        }

        [Test]
        public async Task SetItemsAsync()
        {
            await _cacheService.SetItemsAsync(_mockGames, "games");
            var result = await _cacheService.GetItemsAsync("games");

            result.Count.ShouldBe(3);
            result.ShouldBeOfType<List<Game>>();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task SetItemsAsync_GivenInvalidKey_ShouldThrowArgumentNullException(string key)
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _cacheService.SetItemsAsync(_mockGames, key));
        }
    }
}
