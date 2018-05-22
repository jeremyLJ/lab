using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _06ThreadPriority
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Current thread prority: {0}", Thread.CurrentThread.Priority);
            Console.WriteLine("Running on all cores available");
            RunThreads();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("Running on a single core");
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            RunThreads();

            Console.WriteLine("done...");
            Console.ReadKey();
        }

        static void RunThreads()
        {
            var sample = new ThreadSample();

            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";

            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;

            threadOne.Start();
            threadTwo.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            sample.Stop();
        }

        private class ThreadSample
        {
            private bool isStopped = false;

            public void CountNumbers()
            {
                long counter = 0;

                while (!isStopped)
                {
                    counter++;
                }

                Console.WriteLine("{0} with {1,11} priority has a count = {2,13}",
                    Thread.CurrentThread.Name,
                    Thread.CurrentThread.Priority, counter.ToString("N0"));
            }

            public void Stop()
            {
                isStopped = true;
            }
        }
    }
}
