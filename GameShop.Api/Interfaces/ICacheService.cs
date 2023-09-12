namespace GameShop.Api.Interfaces
{
    public interface ICacheService<T> where T : class
    {
        Task DeleteItemsAsync(string key);
        Task<List<T>?> GetItemsAsync(string key);
        Task SetItemsAsync(List<T> items, string key, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null);
    }
}
