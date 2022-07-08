using BuyBerDinner.Domain.Entities;

namespace BuyBerDinner.Application.Services.Authentication;

public record AuthenticationResult(User User, string Token);