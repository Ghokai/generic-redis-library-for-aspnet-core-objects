using StackExchange.Redis;

namespace RedisManager
{
    public interface IRedisCacheRepository<T>
    {
        string GenerateCacheKey(T model);
        T[] GetValues(string keyPattern);
        T GetValue(string key);
        void SetValue(T model);
        void DeleteValue(string key);
    }
}
