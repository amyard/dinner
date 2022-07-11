using System.Net;

namespace BuyBerDinner.Application.Common.Errors.UsingOneOf;

public record struct DuplicateEmailError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Email already exists.";
}
