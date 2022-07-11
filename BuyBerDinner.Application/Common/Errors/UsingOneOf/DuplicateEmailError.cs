using System.Net;

namespace BuyBerDinner.Application.Common.Errors.UsingOneOf;

public record struct DuplicateEmailError(HttpStatusCode StatusCode, string ErrorMessage) : IError;
