using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1DelegateInThreadPool
{
    class Program
    {
        private delegate string RunOnThreadPool(out int threadId);

        static void Callback(IAsyncResult ar)
        {
            Console.WriteLine("Starting a callback...");
            Console.WriteLine("State passed to a callback:{0}", ar.AsyncState);
            Console.WriteLine("Is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Thread pool worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        static string Test(out int threadId)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("Is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            threadId = Thread.CurrentThread.ManagedThreadId;

            return $"Thread pool worker thread id was:{threadId}";
        }

        static void Main(string[] args)
        {
            int threadId = 0;
            RunOnThreadPool poolDelegate = Test;
            
            var t = new Thread(() => Test(out threadId));
            t.Start();
            t.Join();
            Console.WriteLine("Thread id: {0}", threadId);

            IAsyncResult r = poolDelegate.BeginInvoke(out threadId, Callback, "a delegate asynchronous call");
            //r.AsyncWaitHandle.WaitOne();

            string result = poolDelegate.EndInvoke(out threadId, r);

            Console.WriteLine("Thread pool work thread id:{0}", threadId);
            Console.WriteLine(result);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("Done....");
            Console.ReadKey();
        }
    }
}
