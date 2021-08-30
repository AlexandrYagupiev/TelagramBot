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
            
            //return new WaitingApplicationOrListClickState(bot, chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            NextState = new GroupedListState(bot, chatId);
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
