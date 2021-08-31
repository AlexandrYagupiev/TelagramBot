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
        private readonly AplicationContext aplicationContext;

        public WaitingPhoneNumberState(Bot bot, ApplicationModel application, long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if(e.Message.Text==Commands.Back)
            {
             NextState = new WaitingPriceState(bot, application, chatId, userModel, aplicationContext);
            }
            else if()
            {
             application.User.Phone = e.Message.Text;
             NextState = new WaitingEmailState(bot, application, chatId, userModel, aplicationContext);
            }     
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterPhoneNumber, Commands.Back);
        }
    }
}
