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
       
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient() { Timeout = TimeSpan.FromSeconds(10) };
            client.StartReceiving();
            var me = client.GetMeAsync().Result;
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {        
          
            
        }   

    }      
}

