using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingNameState : State
    {
        private readonly ApplicationModel application;

        public WaitingNameState(Bot bot, ApplicationModel application,long chatId) : base(bot,chatId)
        {
            this.application = application;
        }
        public override State Back()
        {
            return new WaitingCategoryState(bot,application,chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.ProductName = e.Message.Text;
            NextState = new WaitingNameState(bot,application,chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, "Введите краткое описание товара", Commands.Back);
        }
    }
}
