using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.List
{
    public class ListState : State
    {
        public ListState(Bot bot, long chatId) : base(bot,chatId)
        {
            
        }

        public override State Back()
        {
            throw new NotImplementedException();
        }

        protected override void DoAction(MessageEventArgs e)
        {
           
        }

        protected override void PreDoAction()
        {
            var buttonList = new List<string>();
            buttonList.AddRange(Enum.GetNames(typeof(ProductCategoryModel)));
            buttonList.Add(Commands.Back);
            bot.SendButtons(chatId, "Выберете категорию товара", buttonList.ToArray());
        }
    }
}
