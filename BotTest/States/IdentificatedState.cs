using System;
using System.Collections.Generic;
using System.Text;

namespace BotTest.States
{
    public abstract class IdentificatedState:State
    {
        protected readonly long chatId;

        public IdentificatedState(Bot bot, long chatId) : base(bot)
        {
            this.chatId = chatId;
        }
    }
}
