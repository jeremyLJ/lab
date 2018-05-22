using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3SemaphoreSlim
{
    class Program
    {
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

        static void Main(string[] args)
        {
            for (int i = 1; i <= 6; i++)
            {
                string threadName = "Thread" + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
                t.Start();
            }

            Console.WriteLine("done...");
            Console.ReadKey();
        }

        static void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine("{0} waits to access a database", name);
            _semaphore.Wait();
            Console.WriteLine("{0} was granted an access to a database", name);

            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            Console.WriteLine("{0} is completed", name);

            _semaphore.Release();
        }
    }
}
