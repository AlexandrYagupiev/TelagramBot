﻿using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public abstract class State
    {
        protected readonly Bot bot;

        protected readonly long chatId;

        protected abstract void PreDoAction();
        protected abstract void DoAction(MessageEventArgs e);

        private bool IsDoCalled=false;

        protected State NextState=null;

        public State(Bot bot,long chatId)
        {
            this.bot = bot;
            this.chatId = chatId;
            PreDoAction();
        }

        public abstract State Back(); 

        public void Do(MessageEventArgs e)
        {
            IsDoCalled = true;
            DoAction(e);
        }

        public State Next()
        {
            if (NextState == null)
            throw new Exception("DoAction не установил NextState");

            if (!IsDoCalled) 
            throw new Exception("Do не был вызван, вызовите Do");

            return NextState;

        }

    }
}
