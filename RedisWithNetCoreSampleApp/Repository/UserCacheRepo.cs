using RedisWithNetCoreSampleApp.Models;
using RedisManager;
using StackExchange.Redis;

namespace RedisWithNetCoreSampleApp.Repository
{
    public class UserCacheRepo : BaseCacheRepository<User>, IUserCrudRepository
    {
        const string cnstKeyPrefix = "user:";

        public UserCacheRepo(IRedisConnector connector) : base(connector)
        {

        }

        public override string GenerateCacheKey(User model)
        {
            return cnstKeyPrefix + model.UserId;
        }

        public void AddUser(User model)
        {
            this.SetValue(model);
        }

        public void DeleteUser(int id)
        {
            var key = cnstKeyPrefix + id;
            this.DeleteValue(key);
        }

        public User GetUser(int id)
        {
            var key = cnstKeyPrefix + id;
            var u=this.GetValue(key);
            if (u.UserId <= 0)
            {
                return null;
            }
            return u;
        }

        public void UpdateUser(User model)
        {
            this.SetValue(model);
        }

        public User[] GetUsers()
        {
            return this.GetValues(cnstKeyPrefix +"*");
        }
    }
}
