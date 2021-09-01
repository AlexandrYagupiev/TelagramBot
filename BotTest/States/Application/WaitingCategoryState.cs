using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingCategoryState : State
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public WaitingCategoryState(Bot bot,ApplicationModel application,long chatId, UserModel userModel , AplicationContext aplicationContext) : base(bot,chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingApplicationOrListClickState(bot, chatId, userModel, aplicationContext);
            }
            else if (Enum.TryParse<ProductCategoryModel>(e.Message.Text,out var result))
            {
                application.ProductCategory = result;
                NextState = new WaitingNameState(bot, application, chatId, userModel, aplicationContext);
            }
            else
            {
                bot.SendMessage(chatId, Messages.NoCategory);
            }        
        }

        protected override void PreDoAction()
        {
            var buttonList = new List<string>();
            buttonList.AddRange(Enum.GetNames(typeof(ProductCategoryModel)));
            buttonList.Add(Commands.Back);
            bot.SendMessageWithButtons(chatId, Messages.SelectProductCategory, buttonList.ToArray());
        }
    }
}
