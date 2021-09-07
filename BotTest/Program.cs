using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.InputFiles;

namespace BotTest
{
    class Program
    {
        private static string Token { get; set; } = "1905082601:AAELZdNrpxAV9F61wHuiT0z-3icgIVCzcvI";
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token) { Timeout = TimeSpan.FromSeconds(10) };
            client.StartReceiving();
            var bot = new Bot(client, new ImagePathFormatter(@"\photoStorage"), new AplicationContext(@"Server=DESKTOP-4C6PQII\SQLEXPRESS;Database=botDb;Trusted_Connection=True;"));
            while (true) ;
        }

    }      
}

