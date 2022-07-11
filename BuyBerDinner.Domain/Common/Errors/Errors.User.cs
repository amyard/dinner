using ErrorOr;

namespace BuyBerDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail =>
            Error.Conflict(code: "User.DuplicateEmail", description: "Email is in use.");
    }
}