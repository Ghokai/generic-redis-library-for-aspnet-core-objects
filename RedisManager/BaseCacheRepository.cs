using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisManager
{
    public abstract class BaseCacheRepository<T> : IRedisCacheRepository<T>
    {
        private IRedisConnector _redisConnector;

        public BaseCacheRepository(IRedisConnector redisConnector)
        {
            this._redisConnector = redisConnector;
        }

        public virtual void DeleteValue(string key)
        {
            _redisConnector.RemoveFromCache(key);
        }

        public virtual T GetValue(string key)
        {
            return _redisConnector.GetFromCache<T>(key);
        }

        public virtual void SetValue(T model)
        {
            var key = GenerateCacheKey(model);
            _redisConnector.SetToCache<T>(key, model);
        }

        public abstract string GenerateCacheKey(T model);

        public virtual T[] GetValues(string keyPattern)
        {
            List<T> retList = new List<T>();
            IEnumerable<RedisKey> keys=_redisConnector.GetKeys(keyPattern);

            foreach(RedisKey key in keys)
            {
                retList.Add(GetValue(key.ToString()));
            }

            return retList.ToArray();
        }
    }
}
