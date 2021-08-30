using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingDescriptionState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;

        public WaitingDescriptionState(Bot bot, ApplicationModel application, long chatId, UserModel userModel) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }

        public override State Back()
        {
            return new WaitingNameState(bot,application,chatId,userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.Description = e.Message.Text;
            //NextState = new WaitingPhotoPathState(bot, application, chatId, userModel);
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterProductDescription, Commands.Back);
        }
    }
}
