using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisWithNetCoreSampleApp.Models;
using RedisManager;
using RedisWithNetCoreSampleApp.Repository;

namespace RedisWithNetCoreSampleApp.Controllers
{
   
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IUserCrudRepository _repository;

        public UsersController(IUserCrudRepository repository)
        {
            this._repository = repository;
        }
        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {

            var items = _repository.GetUsers();
            return Json(items);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var value = _repository.GetUser(id);
            return Json(value); 
        }
        
        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody]User u)
        {
            _repository.AddUser(u);
            return Ok();
        }
        
        // PUT: api/Users
        [HttpPut]
        public IActionResult Put(  [FromBody]User u)
        {
            _repository.UpdateUser(u);
            return Ok();
        }
        
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteUser(id);
            return Ok();
        }
    }
}
