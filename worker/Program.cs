using System;
using System.Threading;

namespace BASTA.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Doing Work ....");
            Thread.Sleep(10*1000);
            Console.WriteLine("Work Done!");
        }
    }
}
