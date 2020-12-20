using AspNetrCore.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Net5.PracticalDemo.Atrribute;
using Net5.PracticalDemo.Fillter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Controllers
{
    /// <summary>
    /// filter
    /// 1 IResourceFilter-OnResourceExecuting 发生在控制器实例化之前
    /// 2 IActionFilter-OnActionExecuting
    /// 3 IActionFilter-OnActionExecuted
    /// 4 IResultFilter-OnResultExecuting
    /// 5 IResultFilter-OnResultExecuted -- Result是视图替换环节
    /// 6 IResourceFilter-OnResourceExecuted 发生在最后
    /// 
    /// 不同环境都可以做缓存，只是效果不一样，最好是IResourceFilter
    /// 
    /// 基于IResourceFilter完成了缓存，避免了控制器实例化和Action执行，但是视图重新执行了
    /// 
    /// 多个步骤，某个步骤需要缓存结果，Filter不适合
    /// 基于autofac的AOP缓存了service层的结果（适合单个的业务逻辑层，减少访问数据库和接口等）
    /// 
    /// 怎样可以不执行视图，或者直接重用视图的结果，直接重用Html？
    /// ResponseCache: 在请求响应式，添加了一个responseheader，来指导浏览器缓存结果
    /// 
    /// 中间件怎么缓存？
    /// 结合ResponseCache，在中间件完成拦截，可以完全不进入MVC（处理一组请求，跨浏览器等）
    /// </summary>
    public class FifthController : Controller
    {
        #region Identity
        private readonly ILogger<FirstController> _logger;
        private readonly ITestServiceA _testService;
        private readonly IA _a;
        private readonly ITestServiceB _testServiceB;
        
        public FifthController(ILogger<FirstController> logger, ITestServiceA testService, IA A, ITestServiceB testServiceB)
        {
            _logger = logger;
            _testService = testService;
            _a = A;
            _testServiceB = testServiceB;
        }
        #endregion
        #region Index
        [CustomActionFilterAttribute]
        [CustomResultFilter]
        [CustomResourceFilter]
        public IActionResult Index()
        {
            Console.WriteLine("This is Index Action");
            //_logger.LogInformation("This is index");
            return View();
        }
        #endregion

        [CustomActionFilterAttribute] 
        public IActionResult Infomation() 
        {
            _a.Show(111, "jc");
            return View();
        }
        //[CustomCacheResourceFilter]
        //[ResponseCache(Duration = 60)]
        //[CustomCacheResultFilterAttribute(Duration = 30)]
        [ResponseCache(Duration = 600)]//配合服务端缓存（中间件缓存）
        public IActionResult Info() 
        {
            //base.HttpContext.Response.Headers["Cache-Control"] = "public,max-age=600";

            base.ViewBag.Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
            base.ViewBag.ServiceNow = this._a.PlusTime(1,2);
            return View();
        }
    }
}
