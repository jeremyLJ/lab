using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace TestObservable
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadFirstValue(EagerEvaluation());

            //Console.WriteLine("=====================");

            //ReadFirstValue(LazyEvaluation());

            //Console.WriteLine("=====================");
            //SimpleColdSample();

            //ConnectionAndDisconnect();

            //DisconnectByRefCount();

            //ReplayFunction();

            MulticastFunction();

            Console.WriteLine("Done-------");
            Console.Read();
        }

        public static void ReadFirstValue(IEnumerable<int> list)
        {
            foreach (var i in list)
            {
                Console.WriteLine("Read out first value of {0}", i);
                break;
            }
        }

        public static IEnumerable<int> EagerEvaluation()
        {
            var result = new List<int>();
            Console.WriteLine("About to return 1");
            result.Add(1);
            //code below is executed but not used.
            Console.WriteLine("About to return 2");
            result.Add(2);
            return result;
        }

        public static IEnumerable<int> LazyEvaluation()
        {
            Console.WriteLine("About to return 1");
            yield return 1;
            //Execution stops here in this example
            Console.WriteLine("About to return 2");
            yield return 2;
        }

        public static void SimpleColdSample()
        {
            //var period = TimeSpan.FromSeconds(1);
            //var observable = Observable.Interval(period);
            //observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            //Thread.Sleep(period);
            //observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
            /* Output: 
            first subscription : 0 
            first subscription : 1 
            second subscription : 0 
            first subscription : 2 
            second subscription : 1 
            first subscription : 3 
            second subscription : 2 
            */

            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();
            observable.Connect();
            observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            Thread.Sleep(period);
            observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
        }

        public static void ConnectionAndDisconnect()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();
            observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Press enter to connect, esc to exit.");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    var connection = observable.Connect(); //--Connects here--
                    Console.WriteLine("Press any key to dispose of connection.");
                    Console.ReadKey();
                    connection.Dispose(); //--Disconnects here--
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
            }
        }

        public static void DisconnectByRefCount()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period)
                .Do(l => Console.WriteLine("Publishing {0}", l)) //Side effect to show it is running
                .Publish()
                .RefCount();
            //observable.Connect();
            Console.WriteLine("Press any key to subscribe");
            Console.ReadKey();
            var subscription = observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            Console.WriteLine("Press any key to unsubscribe.");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public static void ReplayFunction()
        {
            var period = TimeSpan.FromSeconds(1);
            var hot = Observable.Interval(period)
                .Take(5)
                .Publish();
            hot.Connect();
            Thread.Sleep(period); //Run hot and ensure a value is lost.
            var observable = hot.Replay();
            observable.Connect();
            observable.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            Thread.Sleep(period);
            observable.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
            Console.ReadKey();
            observable.Subscribe(i => Console.WriteLine("third subscription : {0}", i));
            Console.ReadKey();
        }

        public static void MulticastFunction()
        {
            var period = TimeSpan.FromSeconds(1);
            //var observable = Observable.Interval(period).Publish();
            var observable = Observable.Interval(period);
            var shared = new Subject<long>();
            shared.Subscribe(i => Console.WriteLine("first subscription : {0}", i));
            observable.Subscribe(shared);   //'Connect' the observable.
            Thread.Sleep(period);
            Thread.Sleep(period);
            shared.Subscribe(i => Console.WriteLine("second subscription : {0}", i));
        }
    }
}
