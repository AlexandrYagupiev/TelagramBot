using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingEmailState : State
    {
        private readonly ApplicationModel application;

        public WaitingEmailState(Bot bot, ApplicationModel application, long chatId) : base(bot, chatId)
        {
            this.application = application;
        }
        public override State Back()
        {
            return new WaitingPhoneNumberState(bot, application, chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.Email = e.Message.Text;
            NextState = new PreviewApplicationState(bot, application, chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.EnterEmail, Commands.Back);
        }
    }
}
