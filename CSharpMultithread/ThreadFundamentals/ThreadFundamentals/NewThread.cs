using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadFundamentals
{
    class NewThread
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread Id: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread t = new Thread(PrintNumbers);
            t.Start();
            PrintNumbers();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        static void PrintNumbers()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                //Thread.Sleep(10);
                Console.WriteLine("Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, i);
            }
        }
    }
}
