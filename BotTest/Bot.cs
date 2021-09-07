using BotTest.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace BotTest
{
    public class Bot : IDisposable
    {
        private readonly TelegramBotClient telegramBotClient;
        private readonly ImagePathFormatter imagePathFormatter;
        private readonly AplicationContext aplicationContext;

        private State state { get; set; }

        private Dictionary<long, State> chatIdStatePairs;

        public Bot(TelegramBotClient telegramBotClient,ImagePathFormatter imagePathFormatter, AplicationContext aplicationContext)
        {
            this.chatIdStatePairs = new Dictionary<long, State>();
            this.telegramBotClient = telegramBotClient;
            this.imagePathFormatter = imagePathFormatter;
            this.aplicationContext = aplicationContext;
            telegramBotClient.OnMessage += TelegramBotClientOnMessage;          
        }

        private void TelegramBotClientOnMessage(object sender, MessageEventArgs e)
        {
            if (chatIdStatePairs.ContainsKey(e.Message.Chat.Id))
            {
                chatIdStatePairs[e.Message.Chat.Id].Do(e);
                chatIdStatePairs[e.Message.Chat.Id] = chatIdStatePairs[e.Message.Chat.Id].Next();
            }   
            else
            {
                chatIdStatePairs.Add(e.Message.Chat.Id,new StartState(this,e.Message.Chat.Id,aplicationContext));
            }
          
        }

        /// <summary>
        /// Скачивает фото из принятого сообщения, если фото нет возврат пустого листа
        /// </summary>
        /// <param name="e"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public List<PhotoPathModel> DownloadPhotosByMessage(MessageEventArgs e, UserModel userModel)
        {
            var list = new List<PhotoPathModel>();

            for (int i = 0; i < e.Message.Photo.Length; i++)
            {
                var path = imagePathFormatter.GetPath(userModel.Guid, userModel.LastPhoto.NumberInUserFolder + 1, i, "jpg");
                var getFileTask = telegramBotClient.GetFileAsync(e.Message.Photo[i].FileId);
                getFileTask.Wait();
                var stream = File.Create(path);
                var downloadFileTask = telegramBotClient.DownloadFileAsync(getFileTask.Result.FilePath, stream);
                downloadFileTask.Wait();
                stream.Close();
                list.Add(aplicationContext.Photos.Add(new PhotoPathModel() { NumberInUserFolder= userModel.LastPhoto.NumberInUserFolder + 1 ,SizeNumber=i,PhotoPath=path}).Entity);
                aplicationContext.SaveChanges();
            }
            return list;
        }

        public void SendMessageWithButtons(long chatId,string messageText,params string[] buttonNames)
        {
            var task = telegramBotClient.SendTextMessageAsync(
            chatId: chatId,
            text: messageText,
            replyMarkup: GetButtons(buttonNames));
            task.Wait();
        }

        private static IReplyMarkup GetButtons(string[] buttonNames)
        {
            var list = new List<KeyboardButton>();
            for (int i=0;i<buttonNames.Length;i++)
            {
             list.Add(new KeyboardButton {Text=buttonNames[i]});
            }
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                  list
                },
                ResizeKeyboard = true
            };
        }

        public void SendMessage(long chatId,string message)
        {
            var task = telegramBotClient.SendTextMessageAsync(chatId, message);
            task.Wait();
        }

        public void SendApplicationView(long chatId, ApplicationModel application)
        {
            var result = $"Уникальный идинтификатор заявки:{application.Guid}" +
                $"Наименование товара:{application.ProductName}" +
                $"Категория:{application.ProductCategory}" +
                $"Описание:{application.Description}" +
                $"Стоимость:{application.Price}" +
                $"Номер телефона:{application.User.Phone}" +
                $"Email:{application.User.Email}" +
                $"Имя Фамилия:{application.User.FirstName} {application.User.LastName}";
            var task = telegramBotClient.SendTextMessageAsync(chatId, result);
            task.Wait();
        }

        public void SendPhotos(long chatId,List<PhotoPathModel> photoPathModels)
        {
            var list = new List<IAlbumInputMedia>();
            for (var i = 0; i != photoPathModels.Count; i++)
            {
                var photoPath = photoPathModels[i];
                list.Add(new InputMediaPhoto(new InputMedia(File.OpenRead(photoPath.PhotoPath),$"Фото_{i}")));
            }
            var task = telegramBotClient.SendMediaGroupAsync(chatId,list);
            task.Wait();
        }

        public void Dispose()
        {
            telegramBotClient.OnMessage -= TelegramBotClientOnMessage;
        }
    }
}
