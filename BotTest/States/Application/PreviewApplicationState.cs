using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class PreviewApplicationState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;

        public PreviewApplicationState(Bot bot, ApplicationModel application, long chatId, UserModel userModel) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }
        public override State Back()
        {
            return new WaitingEmailState(bot, application, chatId,userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            NextState = new WaitingApplicationOrListClickState(bot, chatId, userModel);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.FormAndSendApplication, Commands.Back);
        }
    }
}
