using BuyBerDinner.Domain.Entities;

namespace BuyBerDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}