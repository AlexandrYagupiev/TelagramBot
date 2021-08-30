using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingEmailState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;

        public WaitingEmailState(Bot bot, ApplicationModel application, long chatId,UserModel userModel) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }
        public override State Back()
        {
            return new WaitingPhoneNumberState(bot, application, chatId, userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {      
            userModel.Email = e.Message.Text;   
            NextState = new PreviewApplicationState(bot, application, chatId, userModel);
           
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterEmail, Commands.Back);
        }
    }
}
