using BuyBerDinner.Api.Filters;
using BuyBerDinner.Application.Services.Authentication;
using BuyBerDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace BuyBerDinner.Api.Controllers;

[Route("auth")]
// [ErrorHandlingFilter] // if need to use only for current controller.
public class AuthenticationController : ApiController
{
    [HttpPost("register")]
    public IActionResult Register([FromServices] IAuthenticationService _authenticationService, 
        RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
            request.FirstName, request.LastName,request.Email, request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }


    [HttpPost("login")]
    public IActionResult Login([FromServices] IAuthenticationService _authenticationService,
        LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(request.Email, request.Password);
        
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
