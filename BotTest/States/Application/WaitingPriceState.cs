using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingPriceState : IdentificatedState
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public WaitingPriceState(Bot bot, ApplicationModel application, long chatId,UserModel userModel,AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if(e.Message.Text==Commands.Back)
            {
                NextState = new WaitingPhotoPathState(bot, application, chatId, userModel, aplicationContext);
            }
            else if(decimal.TryParse(e.Message.Text,out var result))
            {
                if (result < 0)
                {
                    bot.SendMessage(chatId, Messages.AboveZero);
                }
                else
                {
                    application.Price = result;
                    NextState = new WaitingPhoneNumberState(bot, application, chatId, userModel, aplicationContext);
                }
            }
            else
            {
                bot.SendMessage(chatId, Messages.IndicateTheCorrectCost);
            }
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.EnterPriceProduct, Commands.Back);
        }
    }
}
