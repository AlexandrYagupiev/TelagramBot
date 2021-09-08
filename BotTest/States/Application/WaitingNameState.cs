using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingNameState : IdentificatedState
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public WaitingNameState(Bot bot, ApplicationModel application,long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot,chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingCategoryState(bot, application, chatId, userModel, aplicationContext);
            }
            else if(e.Message.Text.Length<100)
            {
                application.ProductName = e.Message.Text;
                NextState = new WaitingDescriptionState(bot, application, chatId, userModel, aplicationContext);
            }
            else if(e.Message.Text.Length >= 100)
            {
                bot.SendMessage(chatId, Messages.TooBigString);
                NextState = new WaitingNameState(bot, application, chatId, userModel, aplicationContext);
            }
            else
            {              
                NextState = new WaitingNameState(bot, application, chatId, userModel, aplicationContext);
            }
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterNameProduct, Commands.Back);
        }
    }
}
