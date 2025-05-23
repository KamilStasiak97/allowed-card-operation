using System.Collections.Generic;

namespace AllowedCardOperation.Domain.Models;

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet); 