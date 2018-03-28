using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacScratch
{
    public interface IMessageHanlder
    {
        void HandlerMessage(Message m);
    }

    public class Message
    {
    }

    public class MessageProcessor
    {
        private readonly IEnumerable<IMessageHanlder> _handlers;

        public MessageProcessor(IEnumerable<IMessageHanlder> handlers)
        {
            this._handlers = handlers;
        }

        public void ProcessMessage(Message m)
        {
            foreach (var handler in _handlers)
            {
                handler.HandlerMessage(m);
            }
        }
    }
}
