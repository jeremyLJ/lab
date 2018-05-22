using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2Mutex
{
    class Program
    {
        const string MutexName = "CSharpThreadingCookbook";

        static void Main(string[] args)
        {
            using (var m = new Mutex(false, MutexName))
            {
                if (!m.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    Console.WriteLine("Second instance is runnning!");
                }
                else
                {
                    Console.WriteLine("Running!");
                    Console.ReadKey();
                    m.ReleaseMutex();
                }
            }

            Console.WriteLine("done");
            Console.ReadKey();
        }

    }
}
