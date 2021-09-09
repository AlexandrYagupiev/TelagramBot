using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.Application
{
    public class PreviewApplicationState : IdentificatedState
    {
        private readonly ApplicationModel application;
        private readonly UserModel userModel;
        private readonly AplicationContext aplicationContext;

        public PreviewApplicationState(Bot bot, ApplicationModel application, long chatId, UserModel userModel, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.application = application;
            this.userModel = userModel;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text == Commands.OK)
            {
                aplicationContext.Applications.Add(application);
                aplicationContext.SaveChanges();
                NextState = new StartState(bot, chatId, aplicationContext);
            }
            else if (e.Message.Text == Commands.Back)
            {
                NextState = new WaitingEmailState(bot, application, chatId, userModel,aplicationContext);
            }
            else
            {
                bot.SendMessage(chatId,Messages.CommandNotRecognized);
                NextState = new PreviewApplicationState(bot, application, chatId, userModel, aplicationContext);
            }
        }

        protected override void PreDoAction()
        {
            bot.SendApplicationView(chatId, application);
            var list = application.PhotoPathes.GroupBy(y=>y.NumberInUserFolder,(t)=>t,(z,x)=> application.PhotoPathes.SingleOrDefault(u=>u.SizeNumber==x.Max(t=>t.SizeNumber)&&u.NumberInUserFolder==z)).ToList();
            bot.SendPhotos(chatId,list);
            bot.SendMessageWithButtons(chatId, Messages.SatisfiedWithTheApplication, Commands.OK, Commands.Cancel, Commands.Back);
        }
    }
}
