using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.List
{
    public class ListState : State
    {
        private readonly Pagination pagination;
        private readonly int numberPage;
        private readonly AplicationContext aplicationContext;

        public ListState(Bot bot, long chatId,Pagination pagination,int numberPage,AplicationContext aplicationContext) : base(bot,chatId)
        {
            this.pagination = pagination;
            this.numberPage = numberPage;
            this.aplicationContext = aplicationContext;
        }

        protected override void DoAction(MessageEventArgs e)
        {
            if (e.Message.Text==Commands.NextPage)
            {
                NextState = new ListState(bot, chatId, pagination, numberPage+1,aplicationContext);
            }
            else if(e.Message.Text==Commands.PreviousPage)
            {
                NextState = new ListState(bot, chatId, pagination, numberPage - 1,aplicationContext);
            }
            else if(e.Message.Text==Commands.Back)
            {
                NextState = new StartState(bot, chatId, aplicationContext);
            }
        }

        protected override void PreDoAction()
        {
            //var buttonList = new List<string>();
            //buttonList.AddRange(Enum.GetNames(typeof(ProductCategoryModel)));
            //buttonList.Add(Commands.Back);
            if (numberPage==0)
            {
                bot.SendMessageWithButtons(chatId, Commands.NextPage, Commands.Back);
            }
            else if(pagination.GetPage(numberPage).Count==0)
            {
                bot.SendMessageWithButtons(chatId, Commands.PreviousPage, Commands.Back);
            }
            else
            {
                bot.SendMessageWithButtons(chatId, Commands.PreviousPage, Commands.NextPage, Commands.Back);
            }
        }
    }
}
