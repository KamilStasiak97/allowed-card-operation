using System.Threading.Tasks;
using AllowedCardOperation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AllowedCardOperation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IAllowedActionsService _allowedActionsService;

    public CardController(ICardService cardService, IAllowedActionsService allowedActionsService)
    {
        _cardService = cardService;
        _allowedActionsService = allowedActionsService;
    }

    [HttpGet("allowed-actions")]
    public async Task<ActionResult<IEnumerable<string>>> GetAllowedActions(
        [FromQuery] string userId,
        [FromQuery] string cardNumber)
    {
        var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
        if (cardDetails == null)
        {
            return NotFound($"Card {cardNumber} not found for user {userId}");
        }
        var actions = _allowedActionsService.GetAllowedActions(cardDetails);
        return Ok(actions);
    }
} 