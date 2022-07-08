using BuyBerDinner.Domain.Entities;

namespace BuyBerDinner.Application.Common.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}