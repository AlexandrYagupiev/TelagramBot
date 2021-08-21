using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotTest.States
{
    public class WaitingForPushButtonsStart : IState
    {
        public WaitingForPushButtonsStart(Bot bot) : base (bot)
        {
            PossibleStates.Add(nameof(WaitingInstitutionCreationOrViewing), new WaitingInstitutionCreationOrViewing(bot));
            Action = (e) => bot.SendTwoButtons();
        }
     
        public override IState Back()
        {
            throw new NotImplementedException();
        }


    }
}
