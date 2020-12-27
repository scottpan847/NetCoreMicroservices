using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Fillter
{
    /// <summary>
    /// OnResourceExecuted 缓存结果
    /// OnResourceExecuting 使用缓存
    /// </summary>
    public class CustomCacheResourceFilterAttribute : Attribute, IResourceFilter, IFilterMetadata, IOrderedFilter
    {
        private static Dictionary<string, IActionResult> _CustomCacheResourceFilterAttributeDictionary = new Dictionary<string, IActionResult>();
        public int Order => 0;

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string key = context.HttpContext.Request.Path;
            if (_CustomCacheResourceFilterAttributeDictionary.ContainsKey(key))
            {
                context.Result = _CustomCacheResourceFilterAttributeDictionary[key];
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;
            _CustomCacheResourceFilterAttributeDictionary.Add(key,context.Result);
        }
    }
}
