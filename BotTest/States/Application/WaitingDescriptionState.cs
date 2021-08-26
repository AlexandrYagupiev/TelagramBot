using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingDescriptionState : State
    {
        private readonly ApplicationModel application;

        public WaitingDescriptionState(Bot bot, ApplicationModel application, long chatId) : base(bot, chatId)
        {
            this.application = application;
        }

        public override State Back()
        {
            return new WaitingNameState(bot,application,chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.Description = e.Message.Text;
            //NextState = new WaitingPhotoPathState(bot,application,chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.EnterProductDescription, Commands.Back);
        }
    }
}
