using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDistributeDemo
{
    //这个类型在被远程访问时会以引用方式封送(MBR)
    public class RemoteMessageObject : MarshalByRefObject
    {
        public RemoteMessageObject()
        {
            Console.WriteLine("Constructing RemoteMessageObject!");
        }

        //这个方法从调用那里获取一个输入字符串
        public void DisplayMessage(string msg)
        {
            Console.WriteLine("Message is:{0}", msg);
        }
        //这个方法把值返回调用方
        public string ReturnMessage()
        {
            return "Hello from the server";
        }
    }

}
