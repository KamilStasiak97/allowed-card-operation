using System;
using System.Threading.Tasks;
using AllowedCardOperation.Application.Interfaces;
using AllowedCardOperation.Domain.Models;
using Moq;
using Xunit;

namespace AllowedCardOperation.Tests;

public class CardServiceTests
{
    private readonly Mock<ICardService> _cardServiceMock;

    public CardServiceTests()
    {
        _cardServiceMock = new Mock<ICardService>();
    }

    [Fact]
    public async Task GetCardDetails_ForPrepaidClosedCardWithPin_ReturnsCardDetails()
    {
        // Arrange
        var userId = "1";
        var cardNumber = "Card11";
        var expectedCard = new CardDetails(cardNumber, CardType.Prepaid, CardStatus.Closed, true);
        _cardServiceMock.Setup(x => x.GetCardDetails(userId, cardNumber))
            .ReturnsAsync(expectedCard);

        // Act
        var result = await _cardServiceMock.Object.GetCardDetails(userId, cardNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCard.CardNumber, result.CardNumber);
        Assert.Equal(expectedCard.CardType, result.CardType);
        Assert.Equal(expectedCard.CardStatus, result.CardStatus);
        Assert.Equal(expectedCard.IsPinSet, result.IsPinSet);
    }

    [Fact]
    public async Task GetCardDetails_ForCreditBlockedCardWithPin_ReturnsCardDetails()
    {
        // Arrange
        var userId = "1";
        var cardNumber = "Card13";
        var expectedCard = new CardDetails(cardNumber, CardType.Credit, CardStatus.Blocked, true);
        _cardServiceMock.Setup(x => x.GetCardDetails(userId, cardNumber))
            .ReturnsAsync(expectedCard);

        // Act
        var result = await _cardServiceMock.Object.GetCardDetails(userId, cardNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCard.CardNumber, result.CardNumber);
        Assert.Equal(expectedCard.CardType, result.CardType);
        Assert.Equal(expectedCard.CardStatus, result.CardStatus);
        Assert.Equal(expectedCard.IsPinSet, result.IsPinSet);
    }

    [Fact]
    public async Task GetCardDetails_ForNonExistentCard_ReturnsNull()
    {
        // Arrange
        var userId = "1";
        var cardNumber = "NonExistentCard";
        _cardServiceMock.Setup(x => x.GetCardDetails(userId, cardNumber))
            .ReturnsAsync((CardDetails?)null);

        // Act
        var result = await _cardServiceMock.Object.GetCardDetails(userId, cardNumber);

        // Assert
        Assert.Null(result);
    }
} 