using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class DeleteApplication : IdentificatedState
    {
        private readonly AplicationContext aplicationContext;

        public DeleteApplication(Bot bot, long chatId, AplicationContext aplicationContext) : base(bot, chatId)
        {
            this.aplicationContext = aplicationContext;
        }
        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text==Commands.Back)
            {
                NextState = new StartState(bot, chatId, aplicationContext);
            }
            else if (Guid.TryParse(e.Message.Text,out var result))
            {
                var application = aplicationContext.Applications.FirstOrDefault((t) => t.Guid == result);
                if (application!=default)
                {
                    aplicationContext.Applications.Remove(application);
                    bot.SendMessage(chatId,Messages.DeleteApplication);                
                }
                else
                {
                    bot.SendMessage(chatId, Messages.ApplicatioNotFound);
                }
                NextState = new DeleteApplication(bot, chatId, aplicationContext);
            }
            else
            {
                bot.SendMessage(chatId, Messages.GuidWrongFormat);
            }

        }

        protected override void PreDoAction()
        {
            bot.SendMessageWithButtons(chatId, Messages.GuidWrite,Commands.Back);
        }
    }
}
