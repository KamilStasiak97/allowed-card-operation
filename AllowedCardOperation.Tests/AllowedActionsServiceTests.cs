using AllowedCardOperation.Application.Interfaces;
using AllowedCardOperation.Domain.Models;
using AllowedCardOperation.Application.Services;
using Xunit;

namespace AllowedCardOperation.Tests;

public class AllowedActionsServiceTests
{
    private readonly IAllowedActionsService _allowedActionsService;

    public AllowedActionsServiceTests()
    {
        _allowedActionsService = new AllowedActionsService();
    }

    [Theory]
    // PREPAID, CLOSED, PIN = true → ACTION3, ACTION4, ACTION9
    [InlineData(CardType.Prepaid, CardStatus.Closed, true, new[] { "ACTION3", "ACTION4", "ACTION9" })]
    
    // CREDIT, BLOCKED, PIN = true → ACTION3, ACTION4, ACTION5, ACTION6, ACTION7, ACTION8, ACTION9
    [InlineData(CardType.Credit, CardStatus.Blocked, true, new[] { "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9" })]
    
    // CREDIT, ACTIVE, PIN = true → WITHOUT ACTION2 i ACTION7
    [InlineData(CardType.Credit, CardStatus.Active, true, new[]
    {
        "ACTION1", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION8", "ACTION9","ACTION10", "ACTION11", "ACTION12", "ACTION13"
    })]
    
    // DEBIT, INACTIVE, PIN = false → WITHOUT ACTION1, ACTION5, ACTION6, 
    [InlineData(CardType.Debit, CardStatus.Inactive, false, new[]
    {
        "ACTION2", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"
    })]
    
    // PREPAID, ACTIVE, PIN = false → WITHOUT ACTION2, ACTION5, ACTION6
    [InlineData(CardType.Prepaid, CardStatus.Active, false, new[]
    {
        "ACTION1", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"
    })]
    
    // DEBIT, BLOCKED, PIN = false → dodaj ACTION3, ACTION4, ACTION8, ACTION9
    [InlineData(CardType.Debit, CardStatus.Blocked, false, new[] {"ACTION3", "ACTION4", "ACTION8", "ACTION9"})]
    
    // PREPAID, EXPIRED, PIN = false → dodaj ACTION3, ACTION4, ACTION9
    [InlineData(CardType.Prepaid, CardStatus.Expired, false, new[] { "ACTION3", "ACTION4", "ACTION9" })]
    public void GetAllowedActions_ForDifferentCardTypes_ReturnsCorrectActions(
        CardType cardType,
        CardStatus cardStatus,
        bool isPinSet,
        string[] expectedActions)
    {
        var cardDetails = new CardDetails(
            CardNumber: "123",
            CardType: cardType,
            CardStatus: cardStatus,
            IsPinSet: isPinSet);

        var result = _allowedActionsService.GetAllowedActions(cardDetails);

        Assert.NotNull(result);
        Assert.Equal(expectedActions.Length, result.Count());
        foreach (var action in expectedActions)
        {
            Assert.Contains(action, result);
        }
    }

    [Fact]
    public void GetAllowedActions_ForNullCardDetails_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _allowedActionsService.GetAllowedActions(null));
    }
}