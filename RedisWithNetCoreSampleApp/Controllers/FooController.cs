using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisWithNetCoreSampleApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Foo")]
    public class FooController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return "Bar";
        }

    }
}