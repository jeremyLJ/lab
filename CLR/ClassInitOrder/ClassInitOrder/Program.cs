using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInitOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start Main()...");

            var a = BaseA.a;
            var b = DriveB.DriveB_b;

            Console.WriteLine("done");
            Console.Read();
        }
    }

    internal class DriveB : BaseA
    {
        public static DisplayClass DriveB_b = new DisplayClass("继承类静态成员DriveB_b初始化");

        DisplayClass DriveB_c = new DisplayClass("继承类实例变量DriveB_c初始化");

        static DriveB()
        {
            Console.WriteLine("继承类静态构造方法被调用");
        }

        public DriveB()
        {
            Console.WriteLine("继承类实例构造方法被调用");
        }
    }

    internal class BaseA
    {
        public static DisplayClass a = new DisplayClass("基类静态成员初始化");
        DisplayClass BaseA_c = new DisplayClass("基类实例变量BaseA_c初始化");

        //static BaseA()
        //{
        //    Console.WriteLine("基类静态构造方法被调用");
        //}

        public BaseA()
        {
            Console.WriteLine("基类实例构造方法被调用");
        }
    }

    internal class DisplayClass
    {
        public DisplayClass(string message)
        {
            Console.WriteLine(message);
        }
    }
}
