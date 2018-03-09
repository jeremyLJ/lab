using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup1>("http://localhost:8888"))
            {
                Console.WriteLine("Press [enter] to exit...");
                Console.ReadLine();
            }
        }
    }
}
