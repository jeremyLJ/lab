using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleDistributeDemo;

namespace SimpleRemoteObjectServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****SimpleRemoteObjectServer started!******");
            Console.WriteLine("Hit enter to end");

            //注册一个新的信道
            HttpChannel c = new HttpChannel(32469);
            ChannelServices.RegisterChannel(c, false);

            //注册一个WKO类型，使用单例激活

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteMessageObject), "RemoteMsgObj.soap", WellKnownObjectMode.Singleton);
            Console.ReadLine();
        }
    }
}
