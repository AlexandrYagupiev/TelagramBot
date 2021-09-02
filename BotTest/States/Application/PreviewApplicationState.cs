using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class PreviewApplicationState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public PreviewApplicationState(Bot bot, ApplicationModel application, long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.OK)
            {
                aplicationContext.Applications.Add(application);
                aplicationContext.SaveChanges();
                NextState = new StartState(bot, chatId,userModel, aplicationContext);
            }
            else if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingEmailState(bot, application, chatId, userModel,aplicationContext);
            }
            else
            {
                bot.SendMessage(chatId,Messages.CommandNotRecognized);
            }
        }

        protected override void PreDoAction()
        {
            bot.SendApplicationView(chatId, application);
            bot.SendPhotos(chatId,application.PhotoPathes);
            bot.SendMessageWithButtons(chatId, Messages.SatisfiedWithTheApplication, Commands.OK, Commands.Cancel, Commands.Back);
        }
    }
}
