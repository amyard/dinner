using BuyBerDinner.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace BuyBerDinner.Application.Common.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;