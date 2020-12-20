using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Service;
using AspNetrCore.Interface;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Net5.PracticalDemo.Atrribute;
using Net5.PracticalDemo.Fillter;

namespace Net5.PracticalDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// ioc容器是目标是设计模式（生成实例），(DI)对象依赖注入是手段，屏蔽细节，解耦
        /// ServiceCollection:内置在Asp.NetCore的一个全新的容器
        /// 
        /// 1 ServiceCollection功能有一定的局限
        /// 2 是新的技术栈，只有构造函数注入，没有方法注入和属性注入（50%替换第三方容器）
        /// 用 autofac 容器
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();//中间件缓存
            //1.全局注册器，全局生效
            services.AddControllersWithViews(
                //options=> options.Filters.Add(typeof(CustomExceptionFilterAtttibutr))
                );
            //2.ServiceFilter||4.IFilterFactory
            //services.AddTransient<CustomExceptionFilterAtttibutr>();
            services.AddSession();
            //services.AddTransient<ITestService, TestServiceB>();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder) 
        {
            containerBuilder.RegisterModule<CustomAutoFacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseResponseCaching();
            app.UseSession();
            loggerFactory.AddLog4Net();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
