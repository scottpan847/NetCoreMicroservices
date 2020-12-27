using AspNetrCore.Interface;
using Consul;
using MicroService.ClientDemo.Models;
using MicroService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MicroService.ClientDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            //base.ViewBag.Users = _userService.UserAll();
            //string url = "http://localhost:5726/api/users/all";
            //string url = "http://localhost:5727/api/users/all";
            //string url = "http://localhost:5728/api/users/all";

            string url = "http://PSJService/api/users/all";//调用组服务
            //其实consul做了什么事情？-> 类似DNS 域名解析
            #region Consul
            //创建Consul客户端
            {
                ConsulClient client = new ConsulClient(c => {
                    c.Address = new Uri("http://127.0.0.1:8500/");
                    c.Datacenter = "dc1";
                });
                var response = client.Agent.Services().Result.Response;
                //foreach (var item in response)
                //{
                //    Console.WriteLine("*********************");
                //    Console.WriteLine(item.Key);
                //    Console.WriteLine(item.Value.Address+"--"+item.Value.Port+"--"+item.Value.Service);
                //    Console.WriteLine("*********************");
                //}
                Uri uri = new Uri(url);
                string groupName = uri.Host;
                var servicesDictionary = response.Where(x => x.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToArray();//找出相对应的组
                
                //{
                //    //url = $"http://{servicesDictionary.First().Value.Address}:{servicesDictionary.First().Value.Port}/api/users/all";//直接拿第一个
                //    url = $"{uri.Scheme}://{servicesDictionary.First().Value.Address}:" +
                //        $"{servicesDictionary.First().Value.Port}{uri.PathAndQuery}";//直接拿第一个
                //}
                //负载均衡策略
                AgentService agentService = null;
                //{
                //    //平均策略 多个实例，平均分配
                //    agentService = servicesDictionary[new Random(++iSeed).Next(0,servicesDictionary.Length)].Value;
                //    //完全平均--轮询策略--很僵化
                //    agentService = servicesDictionary[iSeed++%servicesDictionary.Length].Value;
                //}
                {
                    //根据服务器的情况来分配==权重--不同实例权重不同--配置权重
                    List<KeyValuePair<string, AgentService>> parisList = new List<KeyValuePair<string, AgentService>>();
                    foreach (var para in servicesDictionary)
                    {
                        int count = int.Parse(para.Value.Tags?[0]);
                        for (int i = 0; i < count; i++)
                        {
                            parisList.Add(para);
                        }
                    }
                    agentService = parisList.ToArray()[new Random(iSeed++).Next(0,parisList.Count())].Value;
                }
                url = $"{uri.Scheme}://{agentService.Address}:" +
                        $"{agentService.Port}{uri.PathAndQuery}";

            }
            
            #endregion
            string content = InvokeApi(url);
            base.ViewBag.Users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            Console.WriteLine($"This is {url} Invoke");
            return View();
        }
        private static int iSeed = 0;
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string InvokeApi(string url)
        {
            using (HttpClient httpClient = new HttpClient()) 
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(message).Result;
                return result.Content.ReadAsStringAsync().Result;
            }

        }
    }
}
