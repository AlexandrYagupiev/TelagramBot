using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class StartState : State
    {
        public StartState(Bot bot) : base (bot)
        {
           
        }

        public override State Back()
        {
            return this;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            bot.SendButtons(e.Message.Chat.Id,"Вы хотите создать или посмотреть заявки ?", "Создать заявку", "Список заявок");
            NextState = new ShowButtonsApplicationAndListState(bot);
        }
    }
}
