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
        private readonly AplicationContext aplicationContext;

        public WaitingPhotoPathState(Bot bot, ApplicationModel application, long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.EndOfGettingPhoto)
            {
                NextState = new WaitingPriceState(bot, application, chatId, userModel, aplicationContext);
            }
            else if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingDescriptionState(bot, application, chatId, userModel, aplicationContext);
            }
            else
            {
                var listOfPhotos = bot.DownloadPhotosByMessage(e, userModel);
                if (listOfPhotos.Count!=0)
                {
                    application.PhotoPathes.AddRange(listOfPhotos);
                    NextState = new WaitingPhotoPathState(bot, application, chatId, userModel, aplicationContext);
                }
                else
                {
                    bot.SendMessage(chatId, Messages.NoPhotos);
                }
            }
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.SubmitPhotoProductOrGettingPhoto, Commands.EndOfGettingPhoto, Commands.Back);
        }
    }
}
