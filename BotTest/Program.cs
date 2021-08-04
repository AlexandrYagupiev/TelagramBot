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

namespace BotTest
{
    class Program
    {
        private static string Token { get; set; } = "1905082601:AAELZdNrpxAV9F61wHuiT0z-3icgIVCzcvI";
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token) { Timeout = TimeSpan.FromSeconds(10) };
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            var me = client.GetMeAsync().Result;
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void DownloadFile(string fileId, string path)          
        {

        }

        //private static async void GetMedia(object sender, MessageEventArgs e, Telegram.Bot.Types.InputMediaPhoto inputMedia)
        //{
        //    var test = client.GetFileAsync(e.Message.Photo[e.Message.Photo.Count() - 1].FileId);
        //    var download_url = @"https://api.telegram.org/file/bot<1905082601:AAELZdNrpxAV9F61wHuiT0z-3icgIVCzcvI>/" + test.Result.FilePath;
        //    using (WebClient client = new WebClient())
        //    {
        //        client.DownloadFile(new Uri(download_url), @"c:\NewCompanyPicure.png");
        //    }
        //}


        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {          
            var VehicleType = "";
            var Description = "";
            var Price = "";
            string ImagePath=null;
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Пользователь под Ником: {e.Message.Chat.Username} прислал сообщение {e.Message.Text}");
                if (e.Message.Text == "Создать заявку")
                {                    
                        await client.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Что вы хотите продать ?",
                        replyMarkup: GetButtonsApplication());                
                }

                else if(e.Message.Text == "Список заявок")
                {
                         await client.SendTextMessageAsync(
                         chatId: e.Message.Chat.Id,
                         text: "Список");
                }
                else if(e.Message.Text == "Стационарный компьютер" || e.Message.Text == "Ноутбук" || e.Message.Text == "Монитор" || e.Message.Text == "Клавиатура")
                {
                           await client.SendTextMessageAsync(
                           chatId: e.Message.Chat.Id,
                           text: "Отправьте фото",
                           replyMarkup: GetButtonsSend());
                           VehicleType = e.Message.Text;                          
                }
                else if(e.Message.Text == "Отправить")
                {                     
                        await client.SendTextMessageAsync(
                        chatId: e.Message.Chat.Id,
                        text: "Отправьте фото",
                        replyMarkup: GetButtonsPhoto());                
                }
                else if(e.Message.Text == "Отправить фото")
                {
                            await client.SendTextMessageAsync(
                            chatId: e.Message.Chat.Id,
                            text: "Отправьте фото",
                            replyMarkup: GetButtonsSend());
                }                                          
                else
                {
                await client.SendTextMessageAsync(e.Message.Chat.Id, $"Пожалуйста введите корректную команду", replyMarkup: GetButtonsStart());
                }                   
            }
        }


        private static IReplyMarkup GetButtonsStart()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Создать заявку" }, new KeyboardButton { Text = "Список заявок" } },

                },
                ResizeKeyboard = true
            };
        }
        private static IReplyMarkup GetButtonsApplication()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Стационарный компьютер" }, new KeyboardButton { Text = "Ноутбук" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Монитор" }, new KeyboardButton { Text = "Клавиатура" } },
                },
                ResizeKeyboard = true
            };
        }
        private static IReplyMarkup GetButtonsSend()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Отправить" }, new KeyboardButton { Text = "Назад" } },
                },
            ResizeKeyboard = true
            };
        }

        private static IReplyMarkup GetButtonsPhoto()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Отправить фото" }, new KeyboardButton { Text = "Назад" } },
                },
            ResizeKeyboard = true
            };
        }
        private static IReplyMarkup GetButtonsDescription()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Описание успешно сформировано" }, new KeyboardButton { Text = "Назад" } },
                },
            ResizeKeyboard = true
            };
        }

        private static IReplyMarkup GetButtonsSendRequest()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Отравить заявку" }},
                },
           ResizeKeyboard = true
            };
        }

    }      
}

