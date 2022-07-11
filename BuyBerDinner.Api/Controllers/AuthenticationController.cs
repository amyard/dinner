using BuyBerDinner.Application.Common.Errors.UsingFluentResults;
using BuyBerDinner.Application.Services.Authentication;
using BuyBerDinner.Contracts.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
        Result<AuthenticationResult> registerResult = _authenticationService.Register(
            request.FirstName, request.LastName, request.Email, request.Password);

        if (registerResult.IsSuccess)
            return Ok(MapAuthResult(registerResult.Value));

        var firstError = registerResult.Errors[0];

        if (firstError is DuplicateEmailError)
            return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.");

        return Problem();
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