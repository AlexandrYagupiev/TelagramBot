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
        private readonly UserModel userModel;

        public StartState(Bot bot,long chatId,AplicationContext aplicationContext,UserModel userModel) : base (bot,chatId)
        {
            this.aplicationContext = aplicationContext;
            this.userModel = userModel;
        }

        protected override void PreDoAction()
        {
        }

        protected override void DoAction(MessageEventArgs e)
        {  
            var chat=e.Message.Chat;          
            var userModel=aplicationContext.Users.Single(t => t.TelegramUserName == e.Message.Chat.Username);
            if(userModel is null)
            {
              userModel=aplicationContext.Users.Add(new UserModel() {TelegramUserName=chat.Username,FirstName=chat.FirstName,LastName=chat.LastName}).Entity;
              aplicationContext.SaveChanges();   
              NextState = new WaitingApplicationOrListClickState(bot, chatId,userModel,aplicationContext);
            }
            else if(e.Message.Text==Commands.Back)
            {
                //NextState = new StartState(bot, application, chatId, userModel);
            }
         
        }
    }
}
