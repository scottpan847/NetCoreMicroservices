using AspNetrCore.Interface;
using MicroService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.ServiceInstance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService = null;
        private readonly IConfiguration _configuration;

        public UsersController(ILogger<UsersController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("Get")]
        public User Get(int id) 
        {            return _userService.FindUser(id);
        }
        [HttpGet]
        [Route("All")]
        public List<User> Get()
        {
            Console.WriteLine($"This is UsersController {_configuration["port"]} Invoke");
            return _userService.UserAll().ToList();
        }
    }
}
