using RedisWithNetCoreSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWithNetCoreSampleApp.Repository
{
    public interface IUserCrudRepository
    {
        User[] GetUsers();
        User GetUser(int id);
        void AddUser(User model);
        void UpdateUser(User model);
        void DeleteUser(int id);
    }
}
