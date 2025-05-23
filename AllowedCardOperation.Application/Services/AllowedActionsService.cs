using System;
using System.Collections.Generic;
using System.Linq;
using AllowedCardOperation.Application.Interfaces;
using AllowedCardOperation.Domain.Models;

namespace AllowedCardOperation.Application.Services;

public class AllowedActionsService : IAllowedActionsService
{
    public IEnumerable<string> GetAllowedActions(CardDetails cardDetails)
{
    if (cardDetails == null)
        throw new ArgumentNullException(nameof(cardDetails));

    var actions = new List<string>();
    var type = cardDetails.CardType;
    var status = cardDetails.CardStatus;
    var pin = cardDetails.IsPinSet;

    // ACTION1: YES for ACTIVE
    if (status == CardStatus.Active)
    {
        actions.Add("ACTION1");
    }

    // ACTION2: YES for INACTIVE
    if (status == CardStatus.Inactive)
    {
        actions.Add("ACTION2");
    }

    // ACTION3: YES always
    actions.Add("ACTION3");

    // ACTION4: YES always
    actions.Add("ACTION4");

    // ACTION5: ONLY Credit
    if (type == CardType.Credit)
    {
        actions.Add("ACTION5");
    }

    // ACTION6: YES for ORDERED/INACTIVE/ACTIVE/BLOCKED + PIN - NO without PIN
    if ((status == CardStatus.Ordered || status == CardStatus.Inactive || status == CardStatus.Active || status == CardStatus.Blocked) && pin)
    {
        actions.Add("ACTION6");
    }

    // ACTION7: YES same as ACTION6 but WITHOUT PIN
    if ((status == CardStatus.Ordered || status == CardStatus.Inactive || status == CardStatus.Active) && !pin)
    {
        actions.Add("ACTION7");
    }
    if (status == CardStatus.Blocked && pin)
    {
        actions.Add("ACTION7");
    }

    // ACTION8: NO for RESTRICTED, EXPIRED, CLOSED
    if (status != CardStatus.Restricted && status != CardStatus.Expired && status != CardStatus.Closed)
    {
        actions.Add("ACTION8");
    }

    // ACTION9: YES always
    actions.Add("ACTION9");

    // ACTION10: YES for ORDERED/INACTIVE/ACTIVE
    if (status == CardStatus.Ordered || status == CardStatus.Inactive || status == CardStatus.Active)
    {
        actions.Add("ACTION10");
    }

    // ACTION11: YES for INACTIVE/ACTIVE
    if (status == CardStatus.Inactive || status == CardStatus.Active)
    {
        actions.Add("ACTION11");
    }

    // ACTION12: YES for ORDERED/INACTIVE/ACTIVE
    if (status == CardStatus.Ordered || status == CardStatus.Inactive || status == CardStatus.Active)
    {
        actions.Add("ACTION12");
    }

    // ACTION13: YES for INACTIVE/ACTIVE
    if (status == CardStatus.Ordered || status == CardStatus.Inactive || status == CardStatus.Active)
    {
        actions.Add("ACTION13");
    }

    return actions.Distinct();
}
}