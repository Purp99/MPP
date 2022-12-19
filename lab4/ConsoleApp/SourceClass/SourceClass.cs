using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;

namespace MyCode
{
    public class Class1
    {
        public void FirstMethod()
        {
            Console.WriteLine("First method");
           
        }

        public void SecondMethod()
        {
            Console.WriteLine("Second method");
        }

        public void ThirdMethod(int a)
        {
            Console.WriteLine("Third method (int)");
        }

        public void ThirdMethod(double a)
        {
            Console.WriteLine("Third method (double)");
        }
    }
}

namespace AnotherSpace
{
    public class Class2
    {
        public void FirstMethod()
        {
            Console.WriteLine("First method");
        }

        public void SecondMethod()
        {
            Console.WriteLine("Second method");
        }

        public void ThirdMethod(int a)
        {
            Console.WriteLine("Third method (int)");
        }

        private void ThirdMethod(double a)
        {
            Console.WriteLine("Third method (double)");
        }
    }
}
