using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace KITT_Bot
{
    public class KITTBot
    {
        private readonly string _serverAddress;
        private readonly int _serverPort;
        private readonly string _botIdent;
        private readonly string _botNick;
        private readonly string _botRealName;
        private readonly string _testChannel = "#test321321321";
        private readonly string _botVersionReplay;
        private MessageHandler _messageHandler;

        public KITTBot(string serverAdress, int serverPort, string botIdent, string botNick, string botRealName)
        {
            _serverAddress = serverAdress;
            _serverPort = serverPort;
            _botIdent = botIdent;
            _botRealName = botRealName;
            _botNick = botNick;
            _botVersionReplay = "KITT Bot v0.1";
        }

        public async void Run()
        {
            using (var irc = new TcpClient(_serverAddress, _serverPort))
            using (var stream = irc.GetStream())
            using (var reader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream) { NewLine = "\r\n", AutoFlush = true })
            {
                _messageHandler = new MessageHandler(writer, _botNick);

                await writer.WriteLineAsync($"NICK {_botNick}");
                await writer.WriteLineAsync($"USER {_botIdent} 0 * :{_botRealName}");
                await writer.WriteLineAsync($"JOIN {_testChannel}");

                while (true)
                {
                    string inputLine;
                    while ((inputLine = await reader.ReadLineAsync()) != null)
                    {
                        Console.WriteLine("<- " + inputLine);

                        if (inputLine.Contains("PING :"))
                        {
                            string pongReply = inputLine.Replace("PING", "PONG");
                            await writer.WriteLineAsync(pongReply);
                            Console.WriteLine("-> " + pongReply);
                        }
                        else
                        {
                            await _messageHandler.HandleMessageAsync(inputLine);
                        }
                    }
                    await Task.Delay(100);
                }
            }
        }
    }
}
