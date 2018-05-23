using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _6CountDownEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting two operations");

            var t1 = new Thread(() => PerformOperation("Operation 1 is completed", 4));
            var t2 = new Thread(() => PerformOperation("Operation 2 is completed", 8));
            t1.Start();
            t2.Start();
            _countDown.Wait();

            Console.WriteLine("Both operations have been completed");
            _countDown.Dispose();

            Console.WriteLine("Done...");
            Console.ReadKey();
        }

        static CountdownEvent _countDown = new CountdownEvent(2);

        static void PerformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countDown.Signal();
        }
    }
}
