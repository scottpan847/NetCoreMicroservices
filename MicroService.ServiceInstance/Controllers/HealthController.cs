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
    public class HealthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HealthController( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index() 
        {
            Console.WriteLine($"This is Health {this._configuration["port"]} Invoke");
            return Ok();
        }
        
    }
}
