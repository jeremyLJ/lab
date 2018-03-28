using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleDistributeDemo;

namespace SimpleRemoteObjectClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****SimpleRemoteObjectClient started!******");
            Console.WriteLine("Hit enter to end");
            //注册一个新的信道
            HttpChannel c = new HttpChannel();
            ChannelServices.RegisterChannel(c, false);

            //注册一个WKO类型
            object remoteObj = Activator.GetObject(typeof(RemoteMessageObject), "http://localhost:4313/RemoteMsgObj.soap");
            //现在使用远程对象
            RemoteMessageObject simple = (RemoteMessageObject)remoteObj;
            simple.DisplayMessage("Hello from the client");
            Console.WriteLine("Server says:{0}", simple.ReturnMessage());
            Console.ReadLine();
        }
    }
}
