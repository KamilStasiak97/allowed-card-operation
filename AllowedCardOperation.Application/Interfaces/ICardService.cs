using System.Threading.Tasks;
using AllowedCardOperation.Domain.Models;

namespace AllowedCardOperation.Application.Interfaces;

public interface ICardService
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
} 