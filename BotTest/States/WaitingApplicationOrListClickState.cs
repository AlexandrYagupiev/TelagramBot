using BotTest.States.Application;
using BotTest.States.List;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class WaitingApplicationOrListClickState : State
    {
        public WaitingApplicationOrListClickState(Bot bot, long chatId) : base(bot, chatId)
        {
         
        }

        public override State Back()
        {
            return new StartState(bot,chatId);
        }

        protected override void DoAction(MessageEventArgs e)
        {
           
            if (e.Message.Text==Commands.CreateApplication)
            {             
                NextState = new WaitingCategoryState(bot,new ApplicationModel(),chatId);
            }
            else if(e.Message.Text == Commands.ListOfApplications)
            {
                NextState = new ListState(bot,chatId);
            }

        }

        protected override void PreDoAction()
        {
            bot.SendButtons(chatId, "Вы хотите создать или посмотреть заявки ?", Commands.CreateApplication, Commands.ListOfApplications);
        }
    }
}
