using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KITT_Bot
{
    public class MessageHandler
    {
        private StreamWriter _streamWriter;
        private string _botNick;

        public MessageHandler(StreamWriter streamWriter, string botNick)
        {
            _streamWriter = streamWriter;
            _botNick = botNick;
        }

        internal async Task HandleMessageAsync(string inputLine)
        {
            throw new NotImplementedException();
        }
    }
}
