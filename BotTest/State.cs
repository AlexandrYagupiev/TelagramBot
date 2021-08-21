using System;
using System.Collections.Generic;
using System.Text;

namespace BotTest
{
    public enum State
    {
        WaitingForPushButtonsStart,
        WaitingInstitutionCreationOrViewing,
        WaitingForGroupOfGoodsAdd,
        WaitingForGroupOfGoods,
        WaitingForProductName,
        WaitingForProductDescription,
        WaitingForProductPhoto,
        WaitingForThePriceOfProduct,
        WaitingForPhoneNumber,
        WaitingForEmail,
        ApplicationPreview,
        SendingAnApplication
    }
}
