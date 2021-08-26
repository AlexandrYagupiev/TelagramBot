using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPhoneNumberState : State
    {
        private readonly ApplicationModel application;

        public WaitingPhoneNumberState(Bot bot, ApplicationModel application, long chatId) : base(bot, chatId)
        {
            this.application = application;
        }

        public override State Back()
        {
            return new WaitingPriceState(bot, application, chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.PhoneNumber = e.Message.Text;
            NextState = new WaitingEmailState(bot, application, chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.EnterPhoneNumber, Commands.Back);
        }
    }
}
