using BuyBerDinner.Api.Filters;
using BuyBerDinner.Application.Services.Authentication;
using BuyBerDinner.Application.Services.Authentication.Commands;
using BuyBerDinner.Application.Services.Authentication.Queries;
using BuyBerDinner.Contracts.Authentication;
using BuyBerDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace BuyBerDinner.Api.Controllers;

[Route("auth")]
// [ErrorHandlingFilter] // if need to use only for current controller.
public class AuthenticationController : ApiController
{
    [HttpPost("register")]
    public IActionResult Register(
        [FromServices] IAuthenticationCommandService _authenticationCommandServiceService, 
        RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandServiceService.Register(
            request.FirstName, request.LastName,request.Email, request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }


    [HttpPost("login")]
    public IActionResult Login(
        [FromServices] IAuthenticationQueryService _authenticationQueryService,
        LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationQueryService.Login(request.Email, request.Password);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
    private static AuthenticationResponse MapAuthResult(ErrorOr<AuthenticationResult> authResult)
    {
        return new AuthenticationResponse(
            authResult.Value.User.Id,
            authResult.Value.User.FirstName,
            authResult.Value.User.LastName,
            authResult.Value.User.Email,
            authResult.Value.Token);
    }
}
