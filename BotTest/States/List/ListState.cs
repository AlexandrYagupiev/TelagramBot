using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.List
{
    public class ListState : State
    {
        public ListState(Bot bot) : base(bot)
        {
            
        }

        public override State Back()
        {
            throw new NotImplementedException();
        }

        protected override void DoAction(MessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
