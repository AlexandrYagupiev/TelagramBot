using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class WaitingInstitutionCreationOrViewing : IState
    {
        public WaitingInstitutionCreationOrViewing(Bot bot) : base (bot)
        {
            //PossibleStates.Add(nameof(WaitingForGroupOfGoods), new WaitingForGroupOfGoods(bot));
            PossibleStates.Add(nameof(WaitingForGroupOfGoodsAdd), new WaitingForGroupOfGoodsAdd(bot));
            Action = null;
        }

        public override IState Back()
        {
            throw new NotImplementedException();
        }
    }
}
