using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States.List
{
    public class ListState : IdentificatedState
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
            var list = pagination.GetPage(numberPage);
            for (var i=0;i<list.Count;i++)
            {
                bot.SendApplicationView(chatId, list[i]);
                var listPhoto = list[i].PhotoPathes.GroupBy(y => y.NumberInUserFolder, (t) => t, (z, x) => list[i].PhotoPathes.SingleOrDefault(u => u.SizeNumber == x.Max(t => t.SizeNumber) && u.NumberInUserFolder == z)).ToList();
                bot.SendPhotos(chatId, listPhoto);
            }
        }
    }
}
