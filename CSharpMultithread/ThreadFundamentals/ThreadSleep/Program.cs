using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread Id: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            PrintNumbers();

            Console.WriteLine("done");
            Console.Read();
        }

        static void PrintNumbers()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine("Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, i);
            }
        }

        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, i);
            }
        }
    }
}
