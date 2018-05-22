using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            Drive d1= new Drive();
            d1.PrintMsg();

            Base b = new Drive();
            b.PrintMsg();

            Drive d2 = (Drive)new Base();
            d2.PrintMsg();

            Console.WriteLine("done");
            Console.Read();
        }
    }

    public class Base
    {
        public virtual void PrintMsg()
        {
            Console.WriteLine("Base Printing...");
        }
    }

    public class Drive : Base
    {
        public override void PrintMsg()
        {
            Console.WriteLine("Drive Printing...");
        }
    }
}
