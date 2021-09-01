using BotTest.States.Application;
using BotTest.States.List;
using System;
using System.Collections.Generic;
using System.Linq;
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
            CheckUser(e);
            if (e.Message.Text==Commands.CreateApplication)
            {             
                NextState = new WaitingCategoryState(bot,new ApplicationModel(),chatId,userModel,aplicationContext);
            }
            else if(e.Message.Text == Commands.ListOfApplications)
            {
                NextState = new ListState(bot,chatId);
            }
            else 
            {
                bot.SendMessage(chatId, Messages.CommandNotRecognized);
            }
        }

        /// <summary>
        /// Чек юзера в базе и если его нет, то добавить в базу
        /// </summary>
        /// <param name="e"></param>
        private void CheckUser(MessageEventArgs e)
        {
            var chat = e.Message.Chat;
            var userModel = aplicationContext.Users.Single(t => t.TelegramUserName == e.Message.Chat.Username);
            if (userModel is null)
            {
                userModel = aplicationContext.Users.Add(new UserModel() { TelegramUserName = chat.Username, FirstName = chat.FirstName, LastName = chat.LastName }).Entity;
                aplicationContext.SaveChanges();              
            }
        }
        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.CreateOrViewApplications, Commands.CreateApplication, Commands.ListOfApplications);
        }
    }
}
