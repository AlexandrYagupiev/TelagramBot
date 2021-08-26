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

        public WaitingCategoryState(Bot bot,ApplicationModel application,long chatId, UserModel userModel) : base(bot,chatId)
        {
            this.application = application;
            this.userModel = userModel;
        }
        public override State Back()
        {
            return new WaitingApplicationOrListClickState(bot,chatId,userModel);
        }

        protected override void DoAction(MessageEventArgs e)
        {         
            application.ProductCategory = Enum.Parse<ProductCategoryModel>(e.Message.Text);
            NextState = new WaitingNameState(bot,application,chatId,userModel);           
        }

        protected override void PreDoAction()
        {
            var buttonList = new List<string>();
            buttonList.AddRange(Enum.GetNames(typeof(ProductCategoryModel)));
            buttonList.Add(Commands.Back);
            bot.SendButtons(chatId, Commands.SelectProductCategory, buttonList.ToArray());
        }
    }
}
