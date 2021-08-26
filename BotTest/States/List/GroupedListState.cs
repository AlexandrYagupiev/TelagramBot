using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.List
{
    public class GroupedListState : State
    {
        public GroupedListState(Bot bot, long chatId) : base(bot, chatId)
        {

        }
        public override State Back()
        {
            return new ListState(bot, chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            NextState = new ViewApplicationState(bot, chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, Commands.OutputApplications, Commands.Back);
        }
    }
}
