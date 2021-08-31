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
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public WaitingApplicationOrListClickState(Bot bot, long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
           
            if (e.Message.Text==Commands.CreateApplication)
            {             
                NextState = new WaitingCategoryState(bot,new ApplicationModel(),chatId,userModel,aplicationContext);
            }
            else if(e.Message.Text == Commands.ListOfApplications)
            {
                NextState = new ListState(bot,chatId);
            }
            else if(e.Message.Text==Commands.Back)
            {
                NextState= new StartState(bot, chatId,aplicationContext, userModel);
            }

        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, "Вы хотите создать или посмотреть заявки ?", Commands.CreateApplication, Commands.ListOfApplications);
        }
    }
}
