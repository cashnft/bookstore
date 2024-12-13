// OnlineBookstore.Infrastructure/Cache/RedisCacheService.cs
using System.Text.Json;
using StackExchange.Redis;
using OnlineBookstore.Domain.Interfaces;

namespace OnlineBookstore.Infrastructure.Cache;

public class RedisCacheService : ICacheService  // Make sure this line is exactly like this
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);
        if (value.IsNull)
            return default;
        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        if (expirationTime.HasValue)
            await _db.StringSetAsync(key, serializedValue, expirationTime);
        else
            await _db.StringSetAsync(key, serializedValue);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _db.KeyExistsAsync(key);
    }
}