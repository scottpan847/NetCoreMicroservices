using AspNetrCore.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ITestServiceA _testService;
        private readonly IA _a;
        private readonly ITestServiceB _testServiceB;

        public FirstController(ILogger<FirstController> logger,ITestServiceA testService,IA A, ITestServiceB testServiceB)
        {
            _logger = logger;
            _testService = testService;
            _a = A;
            _testServiceB = testServiceB;
        }
        public IActionResult Index()
        {
            _testService.Run();
            _testServiceB.Run();
            _a.Show(111,"jc");
            return View();
        }
    }
}
