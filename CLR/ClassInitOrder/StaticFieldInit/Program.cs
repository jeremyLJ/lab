using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticFieldInit
{
    class A
    {
        public static int X;

        static A()
        {
            X = B.Y +1;
        }
    }

    class B
    {
        public static int Y = A.X + 1;
        static B() { }

        static void Main()
        {
            Console.WriteLine($"X={A.X}, Y={B.Y}");

            Console.WriteLine("done");
            Console.Read();
        }
    }
}
