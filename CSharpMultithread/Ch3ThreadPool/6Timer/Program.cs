using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _6Timer
{
    class Program
    {
        private static Timer _timer;

        static void TimerOperation(DateTime start)
        {
            var now = DateTime.Now;
            TimeSpan elapsed = now - start;
            Console.WriteLine("[{0}] {1} seconds from {2}. Timer thread pool thread id: {3}",
                now, elapsed.Seconds, start, Thread.CurrentThread.ManagedThreadId);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to stop the timer...");

            DateTime start = DateTime.Now;
            _timer = new Timer(_ => TimerOperation(start), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            Thread.Sleep(TimeSpan.FromSeconds(6));

            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4));

            Console.ReadLine();

            _timer.Dispose();
        }
    }
}
