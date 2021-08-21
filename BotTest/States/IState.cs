using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public abstract class IState
    {
        protected readonly Bot bot;

        public Action<MessageEventArgs> Action { get; set; }

        public IState(Bot bot)
        {
            this.bot = bot;
            PossibleStates = new Dictionary<string, IState>();
        }

        public abstract IState Back(); 

        public Dictionary<string,IState> PossibleStates {get;}

    }
}
