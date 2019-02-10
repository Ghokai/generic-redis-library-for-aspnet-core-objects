using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisManager
{
    public interface IRedisConnector
    {
        IDatabase GetDatabase();
        IServer GetServer();
        HashEntry[] GenerateHash<T>(T obj);
        T MapFromHash<T>(HashEntry[] hash);
        void SetToCache<T>(string key, T obj);
        T GetFromCache<T>(string key);
        void RemoveFromCache(string key);
        IEnumerable<RedisKey> GetKeys(string keyPattern);
    }
}
