using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPriceState : State
    {
        private readonly ApplicationModel application;

        public WaitingPriceState(Bot bot, ApplicationModel application, long chatId) : base(bot, chatId)
        {
            this.application = application;
        }
        public override State Back()
        {
            throw new Exception();
            //return new WaitingPhotoPathState(bot, application, chatId,);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            application.Price = Convert.ToDecimal(e.Message.Text);
            throw new Exception();
            //NextState = new WaitingApplicationOrListClickState(bot, application, chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.EnterPriceProduct, Commands.Back);
        }
    }
}
