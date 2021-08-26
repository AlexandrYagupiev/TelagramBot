using BotTest.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotTest
{
    public class Bot: IDisposable
    {
        private readonly TelegramBotClient telegramBotClient;
        private readonly ImagePathFormatter imagePathFormatter;
        private readonly AplicationContext aplicationContext;

        private State state { get; set; }

        public Bot(TelegramBotClient telegramBotClient,ImagePathFormatter imagePathFormatter, AplicationContext aplicationContext)
        {
            this.telegramBotClient = telegramBotClient;
            this.imagePathFormatter = imagePathFormatter;
            this.aplicationContext = aplicationContext;
            telegramBotClient.OnMessage += TelegramBotClientOnMessage;
            //state = new WaitingForPushButtonsStart();
        }
        private void TelegramBotClientOnMessage(object sender, MessageEventArgs e)
        {
            //state.Action(e);
            //if(state.PossibleStates.Count==1)
            //{
            //    state = state.PossibleStates.First().Value;
            //}
            //else
            //{
            //    //if()
            //    //{

            //    //}
            //}

        }

        public List<PhotoPathModel> DownlodPhotosByMessage(MessageEventArgs e, UserModel userModel)
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

        public void SendButtons(long chatId,string messageText,params string[] buttonNames)
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

        public void Dispose()
        {
            telegramBotClient.OnMessage -= TelegramBotClientOnMessage;
        }
    }
}
