using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Fillter
{
    /// <summary>
    /// 客户端缓存
    /// </summary>
    public class CustomCacheResultFilterAttribute : Attribute, IResultFilter, IFilterMetadata, IOrderedFilter
    {
        //public CustomCacheResultFilterAttribute(int Duration)
        //{
        //    this.Duration = Duration;
        //}
        public int Order => 0;

        public int Duration { get; set; }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //这个不行 已经指定了response
        }


        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["Cache-Control"] = $"public,max-age={this.Duration}";
        }

    }
}
