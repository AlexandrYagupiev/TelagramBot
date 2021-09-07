using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public abstract class State
    {
        protected readonly Bot bot;

        /// <summary>
        /// PreDoAction действие перед срабатыванием триггера
        /// </summary>
        protected abstract void PreDoAction();
        /// <summary>
        /// DoAction обрабатывает триггер
        /// </summary>
        /// <param name="e"></param>
        protected abstract void DoAction(MessageEventArgs e);

        private bool IsDoCalled=false;

        protected State NextState=null;

        public State(Bot bot)
        {
            this.bot = bot;
            PreDoAction();
        }
        /// <summary>
        /// Do вызваеться пользовательским кодом, является триггером
        /// </summary>
        /// <param name="e"></param>
        public void Do(MessageEventArgs e)
        {
            IsDoCalled = true;
            DoAction(e);
        }
        /// <summary>
        /// Вызываеться обязательно после Do, переход к следующему State
        /// </summary>
        /// <returns></returns>
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
