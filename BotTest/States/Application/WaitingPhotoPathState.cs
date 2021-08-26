using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPhotoPathState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;

        public WaitingPhotoPathState(Bot bot, ApplicationModel application, long chatId,UserModel userModel) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }
        public override State Back()
        {
            return new WaitingDescriptionState(bot, application, chatId,userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            
            if(e.Message.Text==Commands.EndOfGettingPhoto)
            {
              NextState = new WaitingPriceState(bot, application, chatId, userModel);
            }
            else
            {
             var listPhoto = bot.DownlodPhotosByMessage(e, userModel);
             application.PhotoPathes.AddRange(listPhoto);
             NextState = new WaitingPhotoPathState(bot, application, chatId, userModel);
            }
   

        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.SubmitPhotoProductOrGettingPhoto, Commands.EndOfGettingPhoto, Commands.Back);
        }
    }
}
