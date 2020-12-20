using AspNetrCore.Interface;
using System;
namespace AspNetCore.Service
{
    public class TestServiceA : ITestServiceA
    {
        public void Eat()
        {
            Console.WriteLine("A Eat Happy!");
        }

        public void Run()
        {
            Console.WriteLine("A Run Happy!");
        }
    }
}
