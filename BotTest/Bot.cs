using BotTest.States;
using System;
using System.Collections.Generic;
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

        private State state { get; set; }

        public Bot(TelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
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
