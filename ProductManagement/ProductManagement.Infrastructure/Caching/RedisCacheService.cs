using ProductManagement.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ProductManagement.Infrastructure.Caching
{
    public class RedisCacheService(IConnectionMultiplexer redis) : ICacheService
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            if (expiration.HasValue)
            {
                await _database.StringSetAsync(key, serializedValue, expiration.Value);
            }
            else
            {
                await _database.StringSetAsync(key, serializedValue);
            }
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            var endpoints = redis.GetEndPoints();
            var server = redis.GetServer(endpoints.First());

            var keys = server.Keys(pattern: $"{prefix}*").ToArray();

            if (keys.Length > 0)
            {
                await _database.KeyDeleteAsync(keys);
            }
        }
    }
}