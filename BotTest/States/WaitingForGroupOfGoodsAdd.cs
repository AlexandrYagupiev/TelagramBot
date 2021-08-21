using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class WaitingForGroupOfGoodsAdd : IState
    {
        public WaitingForGroupOfGoodsAdd(Bot bot) : base(bot)
        {
            //PossibleStates.Add(nameof(), new (bot));
            Action = (e) => bot.SendTwoButtons();
        }

        public override IState Back()
        {
            throw new NotImplementedException();
        }
    }
}
