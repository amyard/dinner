using BuyBerDinner.Application.Common.Errors;
using BuyBerDinner.Application.Common.Interfaces.Authentication;
using BuyBerDinner.Application.Common.Persistence;
using BuyBerDinner.Domain.Entities;

namespace BuyBerDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
            throw new DuplicateEmailException();

        // 2. validate the password is correct
        if (user.Password != password)
            throw new InvalidPasswordException();
        
        // 3. create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1. validate the user doesn't exists.
        if (_userRepository.GetUserByEmail(email) is not null)
            throw new Exception("User with given email already exists.");
            
        // 2. create user & persist to DB
        var user = new User(firstName, lastName, email, password);
        _userRepository.Add(user);
        
        // 3. create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
            
        return new AuthenticationResult(user, token);
    }
}