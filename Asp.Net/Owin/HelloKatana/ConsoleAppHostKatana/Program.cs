using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HelloKatana;

namespace ConsoleAppHostKatana
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseUrl = "http://localhost:5005";

            using (HelloKatana.Start<Startup>(new StartOptions { Url = baseUrl }))
            {
                Console.WriteLine("Press Enter to quit.");
                Console.ReadKey();
            }
        }
    }
}
