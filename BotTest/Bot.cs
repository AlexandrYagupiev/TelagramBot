using BotTest.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotTest
{
    public class Bot: IDisposable
    {
        private readonly TelegramBotClient telegramBotClient;

        private IState state { get; set; }

        public Bot(TelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
            telegramBotClient.OnMessage += TelegramBotClientOnMessage;
            //state = new WaitingForPushButtonsStart();
        }
        private void TelegramBotClientOnMessage(object sender, MessageEventArgs e)
        {
            state.Action(e);
            if(state.PossibleStates.Count==1)
            {
                state = state.PossibleStates.First().Value;
            }
            else
            {
                //if()
                //{

                //}
            }

        }

        public void SendTwoButtons()
        {
            
        }

        public void Dispose()
        {
            telegramBotClient.OnMessage -= TelegramBotClientOnMessage;
        }
    }
}
