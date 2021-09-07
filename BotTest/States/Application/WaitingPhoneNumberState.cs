using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPhoneNumberState : IdentificatedState
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

        private bool CheckNumber(string str)
        {
            if (!str.StartsWith('7') && !str.StartsWith('8')) 
                return false;
            if (str.Length != 11)
                return false;
            return true;
        }


        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingPriceState(bot, application, chatId, userModel, aplicationContext);
            }
            else if (!CheckNumber(e.Message.Text))
            {
                bot.SendMessage(chatId, Messages.InvalidNumberFormat);
            }
            else
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
