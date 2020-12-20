using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Net5.PracticalDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) 
               // .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging((context,logginBuilder)=> 
                    {
                        logginBuilder.AddFilter("System",LogLevel.Warning);//过滤命名空间
                        logginBuilder.AddFilter("Microsoft",LogLevel.Warning);
                        logginBuilder.AddLog4Net();//使用log4net
                    })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(//指定一个web服务器--Kestrel
                webBuilder =>
                {
                    //webBuilder.UseKestrel(o=> { o.Listen(IPAddress.Loopback, 12344); })
                    //.Configure(app=>
                    //app.Run(async context=>
                    //await context.Response.WriteAsync("hello world")))
                    //.UseIIS()
                    //.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
