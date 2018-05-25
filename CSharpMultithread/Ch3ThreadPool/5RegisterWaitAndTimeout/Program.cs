using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5RegisterWaitAndTimeout
{
    class Program
    {
        static void RunOperations(TimeSpan workerOperationTimeOut)
        {
            using (var evt = new ManualResetEvent(false))
            using (var cts = new CancellationTokenSource())
            {
                Console.WriteLine("Registering timeout operations...");
                var worker = ThreadPool.RegisterWaitForSingleObject(evt,
                    (state, isTimeOut) => WorkerOperationWait(cts, isTimeOut), null, workerOperationTimeOut, true);

                Console.WriteLine("Starting long running operation...");
                ThreadPool.QueueUserWorkItem(_ => WorkerOperation(cts.Token, evt));

                Thread.Sleep(workerOperationTimeOut.Add(TimeSpan.FromSeconds(2)));
                worker.Unregister(evt);
            }
        }

        private static void WorkerOperation(CancellationToken token, ManualResetEvent evt)
        {
            for (int i = 0; i < 6; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                Console.WriteLine(i);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            evt.Set();
        }

        private static void WorkerOperationWait(CancellationTokenSource cts, bool isTimeOut)
        {
            if (isTimeOut)
            {
                cts.Cancel();
                Console.WriteLine("Worker operation timed out and was cancelled.");
            }
            else
            {
                Console.WriteLine("Worker operation succeeded.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting....");

            RunOperations(TimeSpan.FromSeconds(5));

            Console.WriteLine("--------------------------------");

            RunOperations(TimeSpan.FromSeconds(7));

            Console.WriteLine("done...");
            Console.ReadKey();
        }
    }
}
