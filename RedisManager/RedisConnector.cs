using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace RedisManager
{
    public class RedisConnector : IRedisConnector
    {
        private ConnectionMultiplexer _redisMultiplexer = null;
        private IDatabase _database = null;
        private IServer _server = null;
        private string _redisConnStr { get; set; }

        public RedisConnector(string redisConnStr)
        {
            _redisConnStr = redisConnStr;
        }

        public IDatabase GetDatabase()
        {
            if (_database == null)
            {
                if (_redisMultiplexer == null)
                {
                    _redisMultiplexer = ConnectionMultiplexer.Connect(_redisConnStr);
                }
                _database = _redisMultiplexer.GetDatabase();
                
            }
            return _database;
        }

        public IServer GetServer()
        {
            if (_server == null)
            {
                if (_redisMultiplexer == null)
                {
                    _redisMultiplexer = ConnectionMultiplexer.Connect(_redisConnStr);
                }
                _server = _redisMultiplexer.GetServer(_redisConnStr);

            }
            return _server;
        }

        public void SetToCache<T>(string key, T obj)
        {
            var hlist = GenerateHash<T>(obj);
            var db = GetDatabase();

            db.HashSet(key, hlist);
        }

        public T GetFromCache<T>(string key)
        {
            var db = GetDatabase();
            var hlist = db.HashGetAll(key);

            return MapFromHash<T>(hlist);
        }

        public void RemoveFromCache(string key)
        {
            var db = GetDatabase();
            db.KeyDelete(key);
        }

        public HashEntry[] GenerateHash<T>(T obj)
        {
            var props = obj.GetType().GetProperties();
            var hash = new HashEntry[props.Count()];

            for (var i = 0; i < props.Count(); i++)
                hash[i] = new HashEntry(props[i].Name, props[i].GetValue(obj).ToString());

            return hash;
        }

        public T MapFromHash<T>(HashEntry[] hash)
        {
            var obj = (T)Activator.CreateInstance(typeof(T)); // new instance of T
            var props = obj.GetType().GetProperties();

            for (var i = 0; i < props.Count(); i++)
            {
                for (var j = 0; j < hash.Count(); j++)
                {
                    if (props[i].Name == hash[j].Name)
                    {
                        var val = hash[j].Value;
                        var type = props[i].PropertyType;

                        if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                            if (string.IsNullOrEmpty(val))
                            {
                                props[i].SetValue(obj, null);
                                
                            }
                        props[i].SetValue(obj, Convert.ChangeType(val, type));
                    }
                }
            }
            return obj;
        }

      
       public IEnumerable<RedisKey> GetKeys(string keyPattern)
        {
            IServer server = this.GetServer();
            IEnumerable<RedisKey> keys = server.Keys(pattern: keyPattern);
            return keys;
        }
    }
}
