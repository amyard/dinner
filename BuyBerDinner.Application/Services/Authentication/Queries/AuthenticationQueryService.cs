using BuyBerDinner.Application.Common.Interfaces.Authentication;
using BuyBerDinner.Application.Common.Persistence;
using BuyBerDinner.Domain.Common.Errors;
using BuyBerDinner.Domain.Entities;
using ErrorOr;

namespace BuyBerDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
            return Errors.Authentication.InvalidCredentials;
        
        // 2. validate the password is correct
        if (user.Password != password)
            return new [] {Errors.Authentication.InvalidCredentials};
        
        // 3. create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }
}