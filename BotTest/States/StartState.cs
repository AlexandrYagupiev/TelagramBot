using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class StartState : State
    {
        public StartState(Bot bot,long chatId) : base (bot,chatId)
        {
           
        }

        public override State Back()
        {
            return this;
        }

        protected override void PreDoAction()
        {
          
        }

        protected override void DoAction(MessageEventArgs e)
        {          
            NextState = new WaitingApplicationOrListClickState(bot,chatId);
        }
    }
}
