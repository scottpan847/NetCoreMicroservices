using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("{id}")]
        public string Get(int id)
        {
            return $"{id}_{_configuration["ip"]}:{_configuration["port"]}";
        }
        [HttpGet]
        //[Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", _configuration["ip"], _configuration["port"] };
        }
    }
}
