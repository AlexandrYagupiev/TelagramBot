using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.List
{
    public class ViewApplicationsState : State
    {
        public ViewApplicationsState(Bot bot, long chatId) : base(bot, chatId)
        {

        }
        protected override void DoAction(MessageEventArgs e)
        {
            NextState = new ListState(bot, chatId);
        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Commands.ViewOtherOrders, Commands.Back);
        }
    }
}
