using BuyBerDinner.Application.Authentication.Common;
using BuyBerDinner.Application.Common.Interfaces.Authentication;
using BuyBerDinner.Application.Common.Persistence;
using BuyBerDinner.Domain.Common.Errors;
using BuyBerDinner.Domain.Entities;
using MediatR;
using ErrorOr;

namespace BuyBerDinner.Application.Common.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // 1. validate the user doesn't exists.
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail;
        
        // 2. create user & persist to DB
        var user = new User(command.FirstName, command.LastName, command.Email, command.Password);
        _userRepository.Add(user);
        
        // 3. create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
            
        return new AuthenticationResult(user, token);
    }
}