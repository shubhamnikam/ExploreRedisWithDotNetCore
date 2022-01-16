using Microsoft.Extensions.Caching.Memory;

namespace API.Redis.MasterSlave.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache;
        public MemoryCacheService(MemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public Task<string> GetCacheValueAsync(string key)
        {
            return Task.FromResult(_cache.Get<string>(key));
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            _cache.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
