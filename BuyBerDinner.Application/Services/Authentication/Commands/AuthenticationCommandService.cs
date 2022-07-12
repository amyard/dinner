using BuyBerDinner.Application.Common.Interfaces.Authentication;
using BuyBerDinner.Application.Common.Persistence;
using BuyBerDinner.Domain.Common.Errors;
using BuyBerDinner.Domain.Entities;
using ErrorOr;

namespace BuyBerDinner.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. validate the user doesn't exists.
        if (_userRepository.GetUserByEmail(email) is not null)
            return Errors.User.DuplicateEmail;
        
        // 2. create user & persist to DB
        var user = new User(firstName, lastName, email, password);
        _userRepository.Add(user);
        
        // 3. create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
            
        return new AuthenticationResult(user, token);
    }
}