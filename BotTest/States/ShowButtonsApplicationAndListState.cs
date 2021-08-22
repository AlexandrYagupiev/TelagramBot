using BotTest.States.Application;
using BotTest.States.List;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class ShowButtonsApplicationAndListState : State
    {
        public ShowButtonsApplicationAndListState(Bot bot) : base (bot)
        {
         
        }

        public override State Back()
        {
            return new StartState(bot);
        }

        protected override void DoAction(MessageEventArgs e)
        {
            var buttonList = new List<string>();
            buttonList.AddRange(Enum.GetNames(typeof(ProductCategoryModel)));
            buttonList.Add("Назад");
            if (e.Message.Text==Commands.CreateApplication)
            {
                bot.SendButtons(e.Message.Chat.Id, "Выберете категорию товара", buttonList.ToArray());
                NextState = new ApplicationState(bot);
            }
            else if(e.Message.Text == Commands.ListOfApplications)
            {
                bot.SendButtons(e.Message.Chat.Id, "Выберете категорию товара", buttonList.ToArray());
                NextState = new ListState(bot);
            }

        }
    }
}
