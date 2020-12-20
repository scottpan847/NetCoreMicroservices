using Microsoft.AspNetCore.Mvc;
using Net5.PracticalDemo.Atrribute;
using Net5.PracticalDemo.Fillter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Controllers
{
    //[CustomExceptionFilterAtttibutr]
    //filter的四种注入方式
    //1.全局注册
    //2.ServiceFilter(加的时候还要ConfigureService)
    //[ServiceFilter(typeof(CustomExceptionFilterAtttibutr))]
    //3.TypeFilter
    //[TypeFilter(typeof(CustomExceptionFilterAtttibutr))]
    //4.IFilterFactory
    [CustomFilterFactory(typeof(CustomExceptionFilterAtttibutr))]
    public class FourController : Controller
    {
        public IActionResult Index()
        {
            int i=0;
            int j = 10;
            var a = j / i;
            return View();
        }
        [CustomActionFilterAttribute]
        public IActionResult Infomation() 
        {
            return View();
        }
    }
}
