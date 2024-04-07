namespace KITT_Bot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var kittBot = new KITTBot(
                serverAdress: "irc.libera.chat",
                serverPort: 6667,
                botIdent: "KITT",
                botRealName: "Just KITT",
                botNick: "KITTBot"
                );

            kittBot.Run();
            Console.ReadKey();
        }
    }
}
