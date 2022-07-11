using BuyBerDinner.Application.Common.Errors.UsingOneOf;
using BuyBerDinner.Application.Services.Authentication;
using BuyBerDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BuyBerDinner.Api.Controllers;

[ApiController]
[Route("auth")]
// [ErrorHandlingFilter] // if need to use only for current controller.
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromServices] IAuthenticationService _authenticationService, 
        RegisterRequest request)
    {
        OneOf<AuthenticationResult, IError> registerResult = _authenticationService.Register(
            request.FirstName, request.LastName, request.Email, request.Password);

        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromServices] IAuthenticationService _authenticationService,
        LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email, 
            request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
        
        return Ok(response);
    }
    
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName,
            authResult.User.LastName, authResult.User.Email, authResult.Token);
    }
}