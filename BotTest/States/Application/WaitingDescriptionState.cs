using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingDescriptionState : IdentificatedState
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public WaitingDescriptionState(Bot bot, ApplicationModel application, long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingNameState(bot, application, chatId, userModel, aplicationContext);
            }
            else if(e.Message.Text.Length<1000)
            {
                application.Description = e.Message.Text;
                NextState = new WaitingPhotoPathState(bot, application, chatId, userModel, aplicationContext);
            }
            else if(e.Message.Text.Length >= 1000)
            {
                bot.SendMessage(chatId, Messages.DescriptionSize);
                NextState = new WaitingDescriptionState(bot, application, chatId, userModel, aplicationContext);
            }
            else
            {
                NextState = new WaitingDescriptionState(bot, application, chatId, userModel, aplicationContext);
            }       
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterProductDescription, Commands.Back);
        }
    }
}
