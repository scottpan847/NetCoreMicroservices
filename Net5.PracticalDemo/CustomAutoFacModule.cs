using AspNetCore.Service;
using AspNetrCore.Interface;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Net5.PracticalDemo
{
    /// <summary>
    /// AutoFac依赖倒置注册模块
    /// </summary>
    public class CustomAutoFacModule : Autofac.Module
    {
        /// <summary>
        /// 重写父类的Load方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();
            containerBuilder.Register(c=>new customAutofacAop());//Aop注册
            containerBuilder.RegisterType<A>().As<IA>().EnableInterfaceInterceptors();
            containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();//感叹，这语法，真的是不能再爽了
            //后面可以注册好多类型……
            //后面可以注册好多类型……
            //后面可以注册好多类型……
            //后面可以注册好多类型……
        }
        
    }
    public class customAutofacAop : IInterceptor
    {
        private static Dictionary<string, object> customAutofacAopDic = new Dictionary<string, object>();
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"method:{invocation.Method}");
            Console.WriteLine($"Arguments={string.Join(",",invocation.Arguments)}");
            string key = $"{invocation.Method}_{string.Join(",", invocation.Arguments)}";
            if (!customAutofacAopDic.ContainsKey(key))
            {
                invocation.Proceed();//继续执行
                customAutofacAopDic.Add(key,invocation.ReturnValue);
            }
            else
            {
                invocation.ReturnValue = customAutofacAopDic[key];
            }
            
            
            Console.WriteLine($"方法{invocation.Method}执行完成了");
        }

    }
    //[Intercept(typeof(customAutofacAop))]
    public interface IA 
    {
        void Show(int id,string name);
        DateTime PlusTime(int i,int j);
    }
    [Intercept(typeof(customAutofacAop))]
    public class A : IA
    {
        public DateTime PlusTime(int i, int j)
        {
            return DateTime.Now;
        }

        public void Show(int id, string name)
        {
            Console.WriteLine("mmd");
        }
    }
}
