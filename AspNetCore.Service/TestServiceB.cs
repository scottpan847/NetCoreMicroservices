using AspNetrCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Service
{
    public class TestServiceB:ITestServiceB
    {
        public TestServiceB()
        {
            Console.WriteLine($"{typeof(TestServiceB)}被构造");
        }
        public void Eat()
        {
            Console.WriteLine("B Eat Happy!");
        }

        public void Run()
        {
            Console.WriteLine("B Run Happy!");
        }
    }
}
