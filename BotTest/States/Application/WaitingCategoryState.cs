using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class WaitingCategoryState : State
    {
        private readonly ApplicationModel application;

        public WaitingCategoryState(Bot bot,ApplicationModel application,long chatId) : base(bot,chatId)
        {
            this.application = application;
        }
        public override State Back()
        {
            return new WaitingApplicationOrListClickState(bot,chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {         
            application.ProductCategory = Enum.Parse<ProductCategoryModel>(e.Message.Text);
            NextState = new WaitingNameState(bot,application,chatId);           
        }

        protected override void PreDoAction()
        {
            var buttonList = new List<string>();
            buttonList.AddRange(Enum.GetNames(typeof(ProductCategoryModel)));
            buttonList.Add("Назад");
            bot.SendButtons(chatId, "Выберете категорию товара", buttonList.ToArray());
        }
    }
}
