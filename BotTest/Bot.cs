using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotTest
{
    public class Bot: IDisposable
    {
        private readonly TelegramBotClient telegramBotClient;
        private Dictionary<State, Action<MessageEventArgs>> stateHandlers;

        private State state { get; set; }

        public Bot(TelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
            telegramBotClient.OnMessage += TelegramBotClientOnMessage;
            state = State.WaitingForPushButtonsStart;
        }

        private void InitializesStateHanglers()
        {
            stateHandlers = new Dictionary<State, Action<MessageEventArgs>>();
            stateHandlers.Add(State.WaitingForEmail,()=>);
            stateHandlers.Add(State.WaitingForGroupOfGoods, () =>);
            stateHandlers.Add(State.WaitingForPhoneNumber, () =>);
            stateHandlers.Add(State.WaitingForProductDescription, () =>);
            stateHandlers.Add(State.WaitingForProductName, () =>);
            stateHandlers.Add(State.WaitingForProductPhoto, () =>);
            stateHandlers.Add(State.WaitingForPushButtonsStart, () =>);
            stateHandlers.Add(State.WaitingForThePriceOfProduct, () =>);
        }

        private void TelegramBotClientOnMessage(object sender, MessageEventArgs e)
        {
            stateHandlers[state](e);           
        }

        public void Dispose()
        {
            telegramBotClient.OnMessage -= TelegramBotClientOnMessage;
        }
    }
}
