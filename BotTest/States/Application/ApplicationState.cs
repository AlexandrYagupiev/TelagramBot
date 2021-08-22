using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class ApplicationState : State
    {
        public ApplicationState(Bot bot) : base(bot)
        {

        }
        public override State Back()
        {
            return new ShowButtonsApplicationAndListState(bot);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            bot.SendButtons(e.Message.Chat.Id, "Введите наименование товара", "Отравить","Назад");
            NextState = new ApplicationState(bot);
        }
    }
}
