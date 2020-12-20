using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Fillter
{
    /// <summary>
    /// exception 特性
    /// </summary>
    public class CustomExceptionFilterAtttibutr:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                Console.WriteLine($"{context.HttpContext.Request.Path}{context.Exception.Message}");
                context.Result = new JsonResult(new {result = false,Msg = "发生异常，请联系管理员" });
                context.ExceptionHandled = true;
            }
        }
    }
}
