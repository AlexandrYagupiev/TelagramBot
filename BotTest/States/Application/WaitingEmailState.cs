using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingEmailState : IdentificatedState
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public WaitingEmailState(Bot bot, ApplicationModel application, long chatId,UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        private bool CheckEmailAddress(string str)
        {
            try
            {
                var m = new MailAddress(str);
                return true;
            }
            catch (FormatException e)
            {
                bot.SendMessage(chatId, e.Message);
                return false;
            }
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingPhoneNumberState(bot, application, chatId, userModel, aplicationContext);
            }
            else if (CheckEmailAddress(e.Message.Text))
            {
                userModel.Email = e.Message.Text;
                NextState = new PreviewApplicationState(bot, application, chatId, userModel, aplicationContext);
            }
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterEmail, Commands.Back);
        }
    }
}
