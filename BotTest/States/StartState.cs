using BotTest.States.Application;
using BotTest.States.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class StartState : State
    {   
        private readonly AplicationContext aplicationContext;

        public StartState(Bot bot, long chatId, AplicationContext aplicationContext) : base(bot, chatId)
        {         
            this.aplicationContext = aplicationContext;
        }


        protected override void DoAction(MessageEventArgs e)
        {         
            if (e.Message.Text==Commands.CreateApplication)
            {             
                NextState = new WaitingCategoryState(bot,new ApplicationModel(),chatId,new UserModel(),aplicationContext);
            }
            else if(e.Message.Text == Commands.ListOfApplications)
            {
                var pagination = new Pagination(aplicationContext,(e)=>true,5);
                NextState = new ListState(bot,chatId,pagination,0,aplicationContext);
            }
            else if(e.Message.Text==Commands.ListOfMyApplications)
            {
                var pagination = new Pagination(aplicationContext, (t) =>t.User.TelegramUserName==e.Message.Chat.Username, 5);
                
                NextState = new ListState(bot, chatId,pagination,0,aplicationContext);
            }
            else if(e.Message.Text==Commands.DeleteApplications)
            {
                NextState = new DeleteApplication(bot, chatId, aplicationContext);
            }
            else 
            {
                bot.SendMessage(chatId, Messages.CommandNotRecognized);
            }
        }      
        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.CreateOrViewApplications, Commands.CreateApplication, Commands.ListOfApplications , Commands.ListOfMyApplications,Commands.DeleteApplications);
        }
    }
}
