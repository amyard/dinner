using System.Net;

namespace BuyBerDinner.Application.Common.Errors.UsingOneOf;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}