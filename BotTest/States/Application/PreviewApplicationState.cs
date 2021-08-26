using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class PreviewApplicationState : State
    {
        private readonly ApplicationModel application;

        public PreviewApplicationState(Bot bot, ApplicationModel application, long chatId) : base(bot, chatId)
        {
            this.application = application;
        }
        public override State Back()
        {
            return new WaitingEmailState(bot, application, chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            NextState = new WaitingApplicationOrListClickState(bot, chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.FormAndSendApplication, Commands.Back);
        }
    }
}
