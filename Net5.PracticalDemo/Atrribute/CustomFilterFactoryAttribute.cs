using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.PracticalDemo
{
    public class CustomFilterFactoryAttribute : Attribute, IFilterFactory
    {
        private readonly Type _type;

        public CustomFilterFactoryAttribute(Type type)
        {
            _type = type;
        }
        public bool IsReusable => true;

        /// <summary>
        /// 容器的实例--构造对象--就可以依赖注入---
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>为了标识， </returns>
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            //serviceProvider是个容器
            return (IFilterMetadata)serviceProvider.GetService(this._type);
            //throw new NotImplementedException();
        }
    }
}
