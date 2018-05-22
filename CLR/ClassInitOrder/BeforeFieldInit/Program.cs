using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeforeFieldInit
{
    class Program
    {
        internal class Foo
        {
            Foo()
            {
                Console.WriteLine("Foo 对象构造函数");
            }
            public static string Field = GetString("初始化 Foo 静态成员变量!");

            public static string GetString(string s)
            {
                Console.WriteLine(s);
                return s;
            }
        }

        internal class FooStatic
        {
            static FooStatic()
            {
                Console.WriteLine("FooStatic 类构造函数");
            }

            FooStatic()
            {
                Console.WriteLine("FooStatic 对象构造函数");
            }

            public static string Field = GetString("初始化 FooStatic 静态成员变量!");

            public static string GetString(string s)
            {
                Console.WriteLine(s);
                return s;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main 开始 ...");

            Foo.GetString("手动调用 Foo.GetString() 方法!");

            FooStatic.GetString("手动调用 FooStatic.GetString() 方法!");

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
