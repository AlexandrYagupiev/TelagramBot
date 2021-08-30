using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPhoneNumberState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;

        public WaitingPhoneNumberState(Bot bot, ApplicationModel application, long chatId, UserModel userModel) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }

        public override State Back()
        {
            return new WaitingPriceState(bot, application, chatId, userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.User.Phone = e.Message.Text;
            NextState = new WaitingEmailState(bot, application, chatId, userModel);
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterPhoneNumber, Commands.Back);
        }
    }
}
