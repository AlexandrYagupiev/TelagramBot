using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingNameState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;

        public WaitingNameState(Bot bot, ApplicationModel application,long chatId, UserModel userModel) : base(bot,chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }
        public override State Back()
        {
            return new WaitingCategoryState(bot,application,chatId,userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.ProductName = e.Message.Text;
            NextState = new WaitingNameState(bot,application,chatId,userModel);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.EnterNameProduct, Commands.Back);
        }
    }
}
