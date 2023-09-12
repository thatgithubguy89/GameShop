using GameShop.Api.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GameShop.Api.Services
{
    public class CacheService<T> : ICacheService<T> where T : class
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task DeleteItemsAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            await _cache.RemoveAsync(key);
        }

        public async Task<List<T>?> GetItemsAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var json = await _cache.GetStringAsync(key);

            if (json == null)
                return null;

            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public async Task SetItemsAsync(List<T> items, string key, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(1);
            options.SlidingExpiration = slidingExpiration ?? TimeSpan.FromMinutes(1);

            var json = JsonSerializer.Serialize(items);

            await _cache.SetStringAsync(key, json);
        }
    }
}