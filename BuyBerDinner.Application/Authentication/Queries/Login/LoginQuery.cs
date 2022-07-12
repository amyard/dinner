using BuyBerDinner.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace BuyBerDinner.Application.Common.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;