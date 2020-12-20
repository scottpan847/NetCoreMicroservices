using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo.Atrribute
{
    public class CustomActionFilterAttribute : Attribute, IActionFilter, IFilterMetadata,IOrderedFilter
    {
        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomActionFilterAttribute)} OnActionExecuted!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomActionFilterAttribute)} OnActionExecuting!");
        }
    }
    public class CustomControllerFilterAttribute : Attribute, IActionFilter, IFilterMetadata
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomControllerFilterAttribute)} OnActionExecuted!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomControllerFilterAttribute)} OnActionExecuting!");
        }
    }
    public class CustomGlobalFilterAttribute : Attribute, IActionFilter, IFilterMetadata
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomGlobalFilterAttribute)} OnActionExecuted!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomGlobalFilterAttribute)} OnActionExecuting!");
        }
    }
    public class CustomResultFilterAttribute : Attribute, IResultFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order { get; set; }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomResultFilterAttribute)} OnResultExecuted!");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomResultFilterAttribute)} OnResultExecuting!");
        }
    }
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order { get; set; }


        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomResourceFilterAttribute)} OnResourceExecuted!");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomResourceFilterAttribute)} OnResourceExecuting!");
        }
    }
}
