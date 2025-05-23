using AllowedCardOperation.Domain.Models;

namespace AllowedCardOperation.Application.Interfaces;

public interface IAllowedActionsService
{
    IEnumerable<string> GetAllowedActions(CardDetails cardDetails);
}