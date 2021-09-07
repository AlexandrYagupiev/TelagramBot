using BotTest.States.Application;
using BotTest.States.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class StartState : IdentificatedState
    {   
        private readonly AplicationContext aplicationContext;

        public StartState(Bot bot, long chatId, AplicationContext aplicationContext) : base(bot, chatId)
        {         
            this.aplicationContext = aplicationContext;
        }

        /// <summary>
        /// Чек юзера в базе и если его нет, то добавить в базу
        /// </summary>
        /// <param name="e"></param>
        private UserModel CheckUser(MessageEventArgs e)
        {
            var chat = e.Message.Chat;
            var userModel = aplicationContext.Users.Single(t => t.TelegramUserName == e.Message.Chat.Username);
            if (userModel is null)
            {
                userModel = aplicationContext.Users.Add(new UserModel() { TelegramUserName = chat.Username, FirstName = chat.FirstName, LastName = chat.LastName }).Entity;
                aplicationContext.SaveChanges();
            }
            return userModel;
        }


        protected override void DoAction(MessageEventArgs e)
        {         
            if (e.Message.Text==Commands.CreateApplication)
            {             
                NextState = new WaitingCategoryState(bot,new ApplicationModel(),chatId,CheckUser(e),aplicationContext);
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
