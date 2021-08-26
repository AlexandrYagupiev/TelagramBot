using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPhotoPathState : State
    {
        private readonly ApplicationModel application;
        private readonly ImagePathFormatter imagePathFormatter;

        public WaitingPhotoPathState(Bot bot, ApplicationModel application, long chatId,ImagePathFormatter imagePathFormatter) : base(bot, chatId)
        {
            this.application = application;
            this.imagePathFormatter = imagePathFormatter;
        }
        public override State Back()
        {
            return new WaitingDescriptionState(bot, application, chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            
            if(e.Message.Text==Commands.AndOfGettingPhoto)
            {
              NextState = new WaitingPriceState(bot, application, chatId);
            }
            //application.PhotoPathes = "";
          
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.SubmitPhotoProductOrGettingPhoto, Commands.AndOfGettingPhoto, Commands.Back);
        }
    }
}
