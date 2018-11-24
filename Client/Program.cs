using System;
using System.Reflection;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.Load("Microsoft.EntityFrameworkCore.Design");


            Console.WriteLine("Hello World!");
        }
    }
}
