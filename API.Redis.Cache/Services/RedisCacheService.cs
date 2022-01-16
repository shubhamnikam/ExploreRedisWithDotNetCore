using StackExchange.Redis;

namespace API.Redis.Cache.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
        }
        public async Task<string> GetCacheValueAsync(string key)
        {
            var db = connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            var db = connectionMultiplexer.GetDatabase();
            db.StringSetAsync(key, value);
            return Task.CompletedTask;
        }
    }
}
